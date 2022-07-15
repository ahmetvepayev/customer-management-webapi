using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using WatermarkService.Core.Application.Interfaces;
using WatermarkService.Core.Domain.Interfaces;

namespace WatermarkService.Core.Application.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public ImageService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public void AddWatermarkToPhoto(int id)
    {
        var customer = _customerRepository.GetById(id);

        if (customer == null)
        {
            return;
        }

        var watermark = "www.mysite.com";
        var pathFont = "../WatermarkService.Core/Application/Fonts/roboto.ttf";

        var fontCollection = new FontCollection();
        var fontFamily = fontCollection.Add(pathFont);

        using(MemoryStream outStream = new MemoryStream())
        using(Image image = Image.Load(customer.Photo, out IImageFormat format))
        {
            float width = image.Width;
            float height = image.Height;
            
            float rectangleY = height*0.92f;
            float rectangleHeight = (height - rectangleY)*0.70f;

            float fontHeight = rectangleHeight*0.80f;

            Font font = fontFamily.CreateFont(fontHeight);

            var fontRectangle = TextMeasurer.Measure(watermark, new TextOptions(font));

            float rectangleX = width*0.02f;
            float rectangleWidth = fontRectangle.Width*1.10f;

            float textX = rectangleX + fontRectangle.Width*0.05f;
            float textY = rectangleY + fontHeight*0.00f;

            RectangleF rectangle = new RectangleF(rectangleX, rectangleY, rectangleWidth, rectangleHeight);

            image.Mutate( x=> {
                x.Fill(Color.White, rectangle);
                x.DrawText(watermark, font, Color.Black, new PointF(textX, textY));
            });

            image.Save(outStream, format);

            customer.Photo = outStream.ToArray();

            _unitOfWork.SaveChanges();
        }
    }
}
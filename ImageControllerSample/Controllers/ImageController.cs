using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using ImageControllerSample.Context;

namespace ImageControllerSample.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/image/
        public async Task<HttpResponseMessage> GetAsync() {
            
            var image = await this.GetImageAsync();
            if (image == null) return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var result = new HttpResponseMessage(HttpStatusCode.OK) {Content = new ByteArrayContent(image)};
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }

        private async Task<byte[]> GetImageAsync() {
            var imageDataProvider = new ImageDataProvider();
            return await imageDataProvider.GetImageAsync();
        }

        // POST api/values
        public async Task Post([FromBody]string value) {
            await this.InsertImageAsync();
        }

        private async Task InsertImageAsync() {
            var filePath = HostingEnvironment.MapPath("~/Images/HT.jpg");
            var fileStream = new FileStream(filePath, FileMode.Open);
            var image = Image.FromStream(fileStream);
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            var imageDataProvider = new ImageDataProvider();
            await imageDataProvider.SaveImageAsync(memoryStream.ToArray());
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }
    }
}

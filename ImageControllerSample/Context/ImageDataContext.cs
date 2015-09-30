using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImageControllerSample.Context
{
    using System.Data.Entity;

    public class ImageDataContext : DbContext
    {
        static ImageDataContext ()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ImageDataContext>());
        }

        public ImageDataContext()
            : base("Name=ImageDataContext") {
            
        }

        public DbSet<ImageObject> ImageObjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<ImageObject>().ToTable("Images");
            modelBuilder.Entity<ImageObject>().HasKey(e => e.ImageKey);
            modelBuilder.Entity<ImageObject>().Property(x => x.Image).HasColumnType("varbinary").HasMaxLength(8000);
        }
    }

    public class ImageObject
    {
        public string ImageKey { get; set; }
        public byte[] Image { get; set; }

        public ImageObject()
        {
            
        }

        public ImageObject(byte[] image) {
            this.ImageKey = Guid.NewGuid().ToString();
            this.Image = image;
        }
    }

    public class ImageDataProvider
    {
        public async Task SaveImageAsync(byte[] image) {
            using (var context = new ImageDataContext()) {
                try {
                    context.ImageObjects.Add(new ImageObject(image));
                    await context.SaveChangesAsync();
                } catch (Exception exception) {

                }

            }
        }

        public async Task<byte[]> GetImageAsync() {
            try {
                using (var context = new ImageDataContext()) {
                    //Keep it simple - return the last image inserted.
                    var imageObject = await context.ImageObjects.FirstOrDefaultAsync();
                    if (imageObject != null) {
                        return imageObject.Image;
                    }
                }
             

            } catch (Exception exception) {

            }
            return null;
        }
    }
}
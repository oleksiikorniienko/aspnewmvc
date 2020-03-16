using System.Data.Entity;
using aspnet2.Services;

namespace aspnet2.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            db.Materials.Add(new Material { Id = 1, Title = "Material 1" });

            for (int i = 0; i < 23; i++)
            {
                db.Products.Add(new Product
                {
                    Title = "Pled Name " + i.ToString(),
                    SizeW = 200,
                    SizeH = 120,
                    MaterialId = 1,
                    InStock = 10,
                    Price = 550
                });
            }

            db.UserTypes.Add(new UserType { Id = UserType.USER, Title = "User"});
            db.UserTypes.Add(new UserType { Id = UserType.ADMIN, Title = "Admin" });

            db.Users.Add(new User {
                Email = "admin@mail.com",
                Password = PasswordService.Hash("admin@mail.com"),
                UserTypeId = UserType.ADMIN
            });

            base.Seed(db);
        }
    }
}

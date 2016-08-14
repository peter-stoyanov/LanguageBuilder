namespace LanguageBuilder.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LanguageBuilder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LanguageBuilder.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var students = new List<Student>
            {
                    new Student{FirstMidName="Carson",LastName="Alexander",
                     EnrollmentDate=DateTime.Parse("2010-09-01")},
                    new Student{FirstMidName="Meredith",LastName="Alonso",
                    EnrollmentDate=DateTime.Parse("2012-09-01")},
                    new Student{FirstMidName="Arturo",LastName="Anand",
                    EnrollmentDate=DateTime.Parse("2013-09-01")},
                    new Student{FirstMidName="Gytis",LastName="Barzdukas",
                    EnrollmentDate=DateTime.Parse("2012-09-01")},
                    new Student{FirstMidName="Yan",LastName="Li",
                    EnrollmentDate=DateTime.Parse("2012-09-01")},
                    new Student{FirstMidName="Peggy",LastName="Justice",
                    EnrollmentDate=DateTime.Parse("2011-09-01")},
                    new Student{FirstMidName="Laura",LastName="Norman",
                    EnrollmentDate=DateTime.Parse("2013-09-01")},
                    new Student{FirstMidName="Nino",LastName="Olivetto",
                    EnrollmentDate=DateTime.Parse("2005-08-11")}
                    };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var DictWord = new List<DictWord>
{
                new DictWord{german_name="Haus",english_name="house"},
                new DictWord{german_name="Tier",english_name="animal"},
                new DictWord{german_name="Gesund",english_name="healthy"},
                new DictWord{german_name="Gesicht",english_name="face"},

};

            DictWord.ForEach(s => context.DictWords.AddOrUpdate(p => p.german_name, s));
            context.SaveChanges();

            var UserWord = new List<UserWord>
                {
                new UserWord{
                StudentID=students.Single(s=>s.LastName=="Alexander").ID,
                DictWordID=DictWord.Single(c=>c.german_name=="Haus").DictWordID,
                Level=1,
                LastReview = DateTime.Parse("2011-09-01"),
                NextReview = DateTime.Parse("2011-09-01")

                },
                new UserWord{
                StudentID=students.Single(s=>s.LastName=="Alonso").ID,
                DictWordID=DictWord.Single(c=>c.german_name=="Gesund").DictWordID,
                Level=2,
                 LastReview = DateTime.Parse("2011-09-01"),
                NextReview = DateTime.Parse("2011-09-01")
                },
                new UserWord{
                StudentID=students.Single(s=>s.LastName=="Justice").ID,
                DictWordID=DictWord.Single(c=>c.german_name=="Gesicht").DictWordID,
                Level=3,
                 LastReview = DateTime.Parse("2011-09-01"),
                NextReview = DateTime.Parse("2011-09-01")
                },
                new UserWord{
                StudentID=students.Single(s=>s.LastName=="Norman").ID,
                DictWordID=DictWord.Single(c=>c.german_name=="Gesund").DictWordID,
                Level=4,
                 LastReview = DateTime.Parse("2011-09-01"),
                NextReview = DateTime.Parse("2011-09-01")
                },
                };

            foreach (UserWord e in UserWord)
            {
                var UserWordInDataBase = context.UserWords.Where(
                s =>
                s.Student.ID == e.StudentID &&
                s.DictWord.DictWordID == e.DictWordID).SingleOrDefault();
                if (UserWordInDataBase == null)
                {
                    context.UserWords.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}

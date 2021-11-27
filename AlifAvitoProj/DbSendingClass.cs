using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlifAvitoProj
{
    public static class DbSendingClass
    {
        public static string SHA1Hash(string hashFunc)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(hashFunc);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                hashFunc = hash;
            }
            return hashFunc;
        }

        public static void SeedDataContext(this AvitoDbContext context)
        {
            var advertsUsers = new List<AdvertUser>()
            {
                new AdvertUser()
                {
                    Advert = new Advert()
                    {
                        Cost = 2000,
                        ReviewText = "2 комнатная квартира возле гулистона за 2000 сомони предоплата за 3 месяца",
                        Title = "2 комнатная квартира",
                        DatePublished = DateTime.UtcNow,
                        AdvertCategories = new List<AdvertCategory>()
                        {
                            new AdvertCategory { Category = new Category() { Name = "Home"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review {
                                Headline = "Не плохая квартира и арендатель",
                                ReviewText = "Квартира стоит своих денег очень большая а также арендатель был очень вежливый",
                                Rating = 5,
                                Reviewer = new Reviewer()
                                {
                                    FirstName = "Ali",
                                    LastName = "Aliev"
                                }
                            },
                            new Review { Headline = "Квартира средняя и не очень", ReviewText = "Квартира стоит своих денег обычная а также арендатель был нормальным", Rating = 1,
                             Reviewer = new Reviewer(){ FirstName = "Amir", LastName = "Amirov" } },
                            new Review { Headline = "Худшая квартира которую я видел", ReviewText = "Очень плохая квартира которая не стоит своих денег", Rating = 3,
                             Reviewer = new Reviewer(){ FirstName = "Baha", LastName = "Bahov" } }
                        }
                    },
                    User = new User()
                    {
                        FirstName = "Сабина",
                        Password = SHA1Hash("810947"),
                        PhoneNumber = 111115555,
                        City = new City()
                        {
                            Name = "Dushanbe"
                        },
                        WarningUser = new WarningUser()
                        {
                            Warning = "Пожалуйста не обманывайте покупателей пешите дословную ифнормацию",
                             Admin = new Admin()
                            {
                                FirstName = "Shahrom",
                                Password = SHA1Hash("9999112")
                            }
                        }
                    }
                },
                new AdvertUser()
                {
                    Advert = new Advert()
                    {
                        Cost = 3000,
                        ReviewText = "ноутбук acer l-460 для монтажа и работы также подойдёт для программирования. SSD-500GB",
                        Title = "ноутбук acer l-460",
                        DatePublished = DateTime.UtcNow,
                        AdvertCategories = new List<AdvertCategory>()
                        {
                            new AdvertCategory { Category = new Category() { Name = "Technology"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headline = "Ноубук классный и не дорогой", ReviewText = "Ноубук классный и не дорогой но нет дискретной видеокарты", Rating = 4,
                             Reviewer = new Reviewer(){ FirstName = "Алишер", LastName = "Алишеров" } }
                        }
                    },
                    User = new User()
                    {
                        FirstName = "Усмон",
                        Password = SHA1Hash("810000"),
                        PhoneNumber = 111115555,
                        City = new City()
                        {
                            Name = "Khorog"
                        }
                    }
                },
                new AdvertUser()
                {
                    Advert = new Advert()
                    {
                        Cost = 5000,
                        ReviewText = "Телефон самсунг S10 в хорошем состоянии Б/У с документами",
                        Title = "Телефон самсунг S10",
                        DatePublished = DateTime.UtcNow,
                        AdvertCategories = new List<AdvertCategory>()
                        {
                            new AdvertCategory { Category = new Category() { Name = "Phone"}},
                            new AdvertCategory { Category = new Category() { Name = "Technology"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { Headline = "Телефон классный и не дорогой", ReviewText = "Телефон классный и не дорогой но процессор очень слабый", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "Кирилл", LastName = "Кириллов" } }
                        }
                    },
                    User = new User()
                    {
                        FirstName = "Мансур",
                        Password = SHA1Hash("111111"),
                        PhoneNumber = 111215555,
                        City = new City()
                        {
                            Name = "Khujand"
                        }
                    }
                },
                new AdvertUser()
                {
                    Advert = new Advert()
                    {
                        Cost = 5000,
                        ReviewText = "Телефон самсунг S20 в хорошем состоянии Б/У с документами",
                        Title = "Телефон самсунг S20",
                        DatePublished = DateTime.UtcNow,
                        AdvertCategories = new List<AdvertCategory>()
                        {
                            new AdvertCategory { Category = new Category() { Name = "Phone"}},
                            new AdvertCategory { Category = new Category() { Name = "Technology"}}
                        }
                    },
                    User = new User(){
                        FirstName = "Абу",
                        Password = SHA1Hash("222222"),
                        PhoneNumber = 123115555,
                        City = new City()
                        {
                            Name = "Kulyab"
                        }
                    }
                },
                new AdvertUser()
                {
                    Advert = new Advert()
                    {
                        Cost = 4500,
                        ReviewText = "Продаю большой Шкаф для вещей в который может поместиться 800л",
                        Title = "Продаю большой Шкаф",
                        DatePublished = DateTime.UtcNow,
                        AdvertCategories = new List<AdvertCategory>()
                        {
                            new AdvertCategory { Category = new Category() { Name = "Всё для дома"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review
                            {
                                Headline = "Большой и вместительный шкаф",
                                ReviewText = "Большой и вместительный шкаф сделан из дуба настоящяя древесина",
                                Rating = 5,
                                Reviewer = new Reviewer()
                                {
                                    FirstName = "АлиАкбар",
                                    LastName = "АлиАкбаров"
                                }
                            },
                            new Review {
                                Headline = "Отвратительный шкаф никому не советую",
                                ReviewText = "Отвратительный шкаф никому не советую не древесина а настоящии опилки не советую никому",
                                Rating = 1,
                             Reviewer = new Reviewer()
                             {
                                 FirstName = "Азиз", LastName = "Азизов" } }
                        }
                    },
                    User = new User()
                    {
                        FirstName = "Фирдавс",
                        Password = SHA1Hash("999223"),
                        PhoneNumber = 211315755,
                        City = new City()
                        {
                            Name = "Kurhan-Tube"
                        },
                        WarningUser = new WarningUser()
                        {
                            Warning = "Пожалуйста не обманывайте покупателей и не нарушайте правила",
                            Admin = new Admin()
                            {
                                FirstName = "Islom",
                                Password = SHA1Hash("1234567")
                            }
                        }
                    }
                }
            };

            context.AdvertUsers.AddRange(advertsUsers);
            context.SaveChanges();
        
        }
    }
}

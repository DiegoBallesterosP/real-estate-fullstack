using MongoDB.Driver;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Persistence
{
    public class DataSeeder
    {
        private readonly IMongoDatabase _database;

        public DataSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            await SeedOwners();
            await SeedProperties();
            await CreateIndexes();
        }

        private async Task SeedOwners()
        {
            var collection = _database.GetCollection<Owner>("Owner");
            await collection.DeleteManyAsync(FilterDefinition<Owner>.Empty);

            if (await collection.CountDocumentsAsync(FilterDefinition<Owner>.Empty) == 0)
            {
                var owners = new List<Owner>
                    {
                        new Owner { Id = "owner001", Name = "Carlos Rodríguez", Address = "Calle Principal 123, CDMX", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1980, 5, 15) },
                        new Owner { Id = "owner002", Name = "María González", Address = "Avenida Reforma 456, CDMX", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1975, 11, 22) },
                        new Owner { Id = "owner003", Name = "Juan Pérez", Address = "Plaza Mayor 789, Guadalajara", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1990, 3, 8) },
                        new Owner { Id = "owner004", Name = "Ana Martínez", Address = "Paseo de la Reforma 101, CDMX", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1985, 7, 12) },
                        new Owner { Id = "owner005", Name = "Luis Hernández", Address = "Av. Insurgentes 202, Monterrey", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1978, 9, 30) },
                        new Owner { Id = "owner006", Name = "Laura García", Address = "Calle Morelos 303, Puebla", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1992, 1, 25) },
                        new Owner { Id = "owner007", Name = "Roberto Silva", Address = "Blvd. Díaz Ordaz 404, Tijuana", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1983, 12, 5) },
                        new Owner { Id = "owner008", Name = "Isabel Castro", Address = "Av. Revolución 505, Mérida", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1970, 6, 18) },
                        new Owner { Id = "owner009", Name = "Miguel Ángel Reyes", Address = "Calle Juárez 606, Cancún", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1988, 2, 14) },
                        new Owner { Id = "owner010", Name = "Sofía Mendoza", Address = "Paseo de Montejo 707, Mérida", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1995, 8, 8) },
                        new Owner { Id = "owner011", Name = "Jorge López", Address = "Av. Hidalgo 808, Querétaro", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1982, 4, 3) },
                        new Owner { Id = "owner012", Name = "Carmen Vargas", Address = "Blvd. Aguacaliente 909, Tijuana", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1973, 10, 17) },
                        new Owner { Id = "owner013", Name = "Ricardo Flores", Address = "Av. Universidad 1010, Puebla", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1987, 11, 9) },
                        new Owner { Id = "owner014", Name = "Elena Torres", Address = "Calle 60 1111, Mérida", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1993, 7, 22) },
                        new Owner { Id = "owner015", Name = "Fernando Navarro", Address = "Av. López Mateos 1212, Guadalajara", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1981, 12, 31) },
                        new Owner { Id = "owner016", Name = "Patricia Ruiz", Address = "Paseo de los Leones 1313, León", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1976, 5, 19) },
                        new Owner { Id = "owner017", Name = "Diego Morales", Address = "Av. Constituyentes 1414, CDMX", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1989, 9, 7) },
                        new Owner { Id = "owner018", Name = "Adriana Cervantes", Address = "Blvd. García Sancho 1515, Aguascalientes", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1991, 3, 28) },
                        new Owner { Id = "owner019", Name = "Oscar Ramírez", Address = "Av. Tecnológico 1616, Monterrey", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1984, 6, 11) },
                        new Owner { Id = "owner020", Name = "Gabriela Ortega", Address = "Calle 5 de Mayo 1717, Morelia", Photo = "https://images.icon-icons.com/1674/PNG/512/person_110935.png", Birthday = new DateTime(1979, 2, 4) }
                    };

                await collection.InsertManyAsync(owners);
                Console.WriteLine("20 owners seeded successfully!");
            }
        }

        private async Task SeedProperties()
        {
            var collection = _database.GetCollection<Property>("Property");
            await collection.DeleteManyAsync(FilterDefinition<Property>.Empty);

            if (await collection.CountDocumentsAsync(FilterDefinition<Property>.Empty) == 0)
            {


                var properties = new List<Property>
                    {
                        new Property { Id = "property001", Name = "Casa en Condesa", Address = "Av. Michoacán 123, CDMX", Price = 8500000.00m, CodeInternal = "PROP-CDMX-001", Year = 2024, OwnerId = "owner001", Images = new List<PropertyImage> { new PropertyImage { Id = "img001", File = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&h=600&fit=crop", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace001", DateSale = new DateTime(2024, 1, 15), Name = "Venta Inicial", Value = 8500000.00m, Tax = 680000.00m } } },
                        new Property { Id = "property002", Name = "Departamento Polanco", Address = "Campos Elíseos 456, CDMX", Price = 12000000.00m, CodeInternal = "PROP-CDMX-002", Year = 2023, OwnerId = "owner002", Images = new List<PropertyImage> { new PropertyImage { Id = "img002", File = "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace002", DateSale = new DateTime(2023, 11, 20), Name = "Venta de Lujo", Value = 12000000.00m, Tax = 960000.00m } } },
                        new Property { Id = "property003", Name = "Loft Roma Norte", Address = "Calle Oaxaca 789, CDMX", Price = 6500000.00m, CodeInternal = "PROP-CDMX-003", Year = 2024, OwnerId = "owner003", Images = new List<PropertyImage> { new PropertyImage { Id = "img003", File = "https://static.vecteezy.com/system/resources/thumbnails/022/880/529/small/ai-generative-3d-modern-luxury-real-estate-house-for-sale-and-rent-luxury-property-concept-photo.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace003", DateSale = new DateTime(2024, 2, 10), Name = "Venta Rápida", Value = 6500000.00m, Tax = 520000.00m } } },

                        new Property { Id = "property004", Name = "Casa en la Playa", Address = "Av. Constituyentes 101, Playa del Carmen", Price = 3500000.00m, CodeInternal = "PROP-PDC-001", Year = 2024, OwnerId = "owner004", Images = new List<PropertyImage> { new PropertyImage { Id = "img004", File = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800&h=600&fit=crop", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace004", DateSale = new DateTime(2024, 3, 5), Name = "Venta Vacacional", Value = 3500000.00m, Tax = 280000.00m } } },
                        new Property { Id = "property005", Name = "Departamento Beachfront", Address = "Calle 10 Norte 202, Playa del Carmen", Price = 2800000.00m, CodeInternal = "PROP-PDC-002", Year = 2023, OwnerId = "owner005", Images = new List<PropertyImage> { new PropertyImage { Id = "img005", File = "https://images.unsplash.com/photo-1600585154526-990dced4db0d?w=800&h=600&fit=crop", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace005", DateSale = new DateTime(2023, 12, 15), Name = "Venta Ejecutiva", Value = 2800000.00m, Tax = 224000.00m } } },

                        new Property { Id = "property006", Name = "Casa en Providencia", Address = "Av. Américas 303, Guadalajara", Price = 4200000.00m, CodeInternal = "PROP-GDL-001", Year = 2024, OwnerId = "owner006", Images = new List<PropertyImage> { new PropertyImage { Id = "img006", File = "https://images.adsttc.com/media/images/51ba/9f8b/b3fc/4b11/7900/0053/newsletter/14.jpg?1371185033", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace006", DateSale = new DateTime(2024, 1, 25), Name = "Venta Familiar", Value = 4200000.00m, Tax = 336000.00m } } },
                        new Property { Id = "property007", Name = "Departamento Chapalita", Address = "Av. Guadalupe 404, Guadalajara", Price = 3800000.00m, CodeInternal = "PROP-GDL-002", Year = 2023, OwnerId = "owner007", Images = new List<PropertyImage> { new PropertyImage { Id = "img007", File = "https://www.epsicu.com/wp-content/uploads/2023/05/viviendas-pasivas-de-lujo-.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace007", DateSale = new DateTime(2023, 10, 30), Name = "Venta Inversión", Value = 3800000.00m, Tax = 304000.00m } } },

                        new Property { Id = "property008", Name = "Casa en San Pedro", Address = "Av. Gómez Morín 505, Monterrey", Price = 5500000.00m, CodeInternal = "PROP-MTY-001", Year = 2024, OwnerId = "owner008", Images = new List<PropertyImage> { new PropertyImage { Id = "img008", File = "https://images.unsplash.com/photo-1600566753086-00f18fb6b3ea?w=800&h=600&fit=crop", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace008", DateSale = new DateTime(2024, 2, 18), Name = "Venta Premium", Value = 5500000.00m, Tax = 440000.00m } } },
                        new Property { Id = "property009", Name = "Departamento Centro", Address = "Av. Madero 606, Monterrey", Price = 3200000.00m, CodeInternal = "PROP-MTY-002", Year = 2023, OwnerId = "owner009", Images = new List<PropertyImage> { new PropertyImage { Id = "img009", File = "https://img4.idealista.com/blur/480_360_mq/0/id.pro.es.image.master/de/3c/a5/1339031243.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace009", DateSale = new DateTime(2023, 9, 12), Name = "Venta Comercial", Value = 3200000.00m, Tax = 256000.00m } } },

                        new Property { Id = "property010", Name = "Casa en Norte", Address = "Paseo de Montejo 707, Mérida", Price = 2900000.00m, CodeInternal = "PROP-MID-001", Year = 2024, OwnerId = "owner010", Images = new List<PropertyImage> { new PropertyImage { Id = "img010", File = "https://thumbs.dreamstime.com/b/sola-nueva-casa-grande-de-dos-pisos-hermosa-118595105.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace010", DateSale = new DateTime(2024, 3, 8), Name = "Venta Tradicional", Value = 2900000.00m, Tax = 232000.00m } } },
                        new Property { Id = "property011", Name = "Departamento Centro Histórico", Address = "Calle 60 808, Mérida", Price = 2100000.00m, CodeInternal = "PROP-MID-002", Year = 2023, OwnerId = "owner011", Images = new List<PropertyImage> { new PropertyImage { Id = "img011", File = "https://thumbs.dreamstime.com/b/casa-1455646.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace011", DateSale = new DateTime(2023, 8, 22), Name = "Venta Cultural", Value = 2100000.00m, Tax = 168000.00m } } },

                        new Property { Id = "property012", Name = "Casa en Playas", Address = "Blvd. Costero 909, Tijuana", Price = 3300000.00m, CodeInternal = "PROP-TIJ-001", Year = 2024, OwnerId = "owner012", Images = new List<PropertyImage> { new PropertyImage { Id = "img012", File = "https://thumbs.dreamstime.com/b/casa-del-ladrillo-rojo-en-el-ajuste-enselvado-con-las-calabazas-61522780.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace012", DateSale = new DateTime(2024, 1, 30), Name = "Venta Fronteriza", Value = 3300000.00m, Tax = 264000.00m } } },
                        new Property { Id = "property013", Name = "Departamento Zona Río", Address = "Av. Paseo de los Héroes 1010, Tijuana", Price = 2700000.00m, CodeInternal = "PROP-TIJ-002", Year = 2023, OwnerId = "owner013", Images = new List<PropertyImage> { new PropertyImage { Id = "img013", File = "https://elegant.decorexpro.com/wp-content/uploads/2018/01/Dom-odnoetazhnyj-s-mansardoj-060-e1518552788640.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace013", DateSale = new DateTime(2023, 11, 5), Name = "Venta Internacional", Value = 2700000.00m, Tax = 216000.00m } } },

                        new Property { Id = "property014", Name = "Casa en Angelópolis", Address = "Av. Juárez 1111, Puebla", Price = 3100000.00m, CodeInternal = "PROP-PUE-001", Year = 2024, OwnerId = "owner014", Images = new List<PropertyImage> { new PropertyImage { Id = "img014", File = "https://invierteconalejandra.com/wp-content/uploads/2021/08/new-home-1553256_640.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace014", DateSale = new DateTime(2024, 2, 28), Name = "Venta Moderna", Value = 3100000.00m, Tax = 248000.00m } } },
                        new Property { Id = "property015", Name = "Departamento Centro", Address = "Calle 5 de Mayo 1212, Puebla", Price = 1900000.00m, CodeInternal = "PROP-PUE-002", Year = 2023, OwnerId = "owner015", Images = new List<PropertyImage> { new PropertyImage { Id = "img015", File = "https://previews.123rf.com/images/qingwa/qingwa1112/qingwa111200642/11379560-smaller-single-family-home-in-suburban-maryland-usa.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace015", DateSale = new DateTime(2023, 7, 14), Name = "Venta Histórica", Value = 1900000.00m, Tax = 152000.00m } } },

                        new Property { Id = "property016", Name = "Casa en Zona Hotelera", Address = "Blvd. Kukulcán 1313, Cancún", Price = 4800000.00m, CodeInternal = "PROP-CUN-001", Year = 2024, OwnerId = "owner016", Images = new List<PropertyImage> { new PropertyImage { Id = "img016", File = "https://i.ytimg.com/vi/RsrvO2CF-3s/sddefault.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace016", DateSale = new DateTime(2024, 3, 12), Name = "Venta Turística", Value = 4800000.00m, Tax = 384000.00m } } },
                        new Property { Id = "property017", Name = "Departamento Puerto Cancún", Address = "Av. Bonampak 1414, Cancún", Price = 3600000.00m, CodeInternal = "PROP-CUN-002", Year = 2023, OwnerId = "owner017", Images = new List<PropertyImage> { new PropertyImage { Id = "img017", File = "https://i.pinimg.com/736x/75/a0/56/75a05651d7413242547f0dc331efe607.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace017", DateSale = new DateTime(2023, 10, 8), Name = "Venta Residencial", Value = 3600000.00m, Tax = 288000.00m } } },

                        new Property { Id = "property018", Name = "Casa en Juriquilla", Address = "Blvd. Bernardo Quintana 1515, Querétaro", Price = 3700000.00m, CodeInternal = "PROP-QRO-001", Year = 2024, OwnerId = "owner018", Images = new List<PropertyImage> { new PropertyImage { Id = "img018", File = "https://st4.depositphotos.com/2249091/20561/i/1600/depositphotos_205619614-stock-photo-garden-driveway-english-house-forest.jpg", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace018", DateSale = new DateTime(2024, 2, 22), Name = "Venta Residencial", Value = 3700000.00m, Tax = 296000.00m } } },
                        new Property { Id = "property019", Name = "Departamento Centro", Address = "Av. Corregidora 1616, Querétaro", Price = 2500000.00m, CodeInternal = "PROP-QRO-002", Year = 2023, OwnerId = "owner019", Images = new List<PropertyImage> { new PropertyImage { Id = "img019", File = "https://img.freepik.com/fotos-premium/casa-moderna-hermoso-diseno-interior-ventana-fondo-colorido-arbol_1236215-22045.jpg?semt=ais_hybrid&w=740&q=80", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace019", DateSale = new DateTime(2023, 6, 19), Name = "Venta Colonial", Value = 2500000.00m, Tax = 200000.00m } } },

                        new Property { Id = "property020", Name = "Casa en Campestre", Address = "Blvd. Campestre 1717, León", Price = 2900000.00m, CodeInternal = "PROP-LEO-001", Year = 2024, OwnerId = "owner020", Images = new List<PropertyImage> { new PropertyImage { Id = "img020", File = "https://img.pikbest.com/ai/illus_our/20230423/4c144f0ecf8bef12ed836b7cd5fa249d.jpg!w700wp", Enabled = true } }, Traces = new List<PropertyTrace> { new PropertyTrace { Id = "trace020", DateSale = new DateTime(2024, 3, 15), Name = "Venta Exclusiva", Value = 2900000.00m, Tax = 232000.00m } } }
                    };

                await collection.InsertManyAsync(properties);
                Console.WriteLine("20 properties seeded successfully!");
            }
        }

        private async Task CreateIndexes()
        {
            var propertiesCollection = _database.GetCollection<Property>("Property");

            try
            {
                await propertiesCollection.Indexes.DropOneAsync("Name_text");
                await propertiesCollection.Indexes.DropOneAsync("Address_text");
                Console.WriteLine("Previous indexes removed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not remove previous indexes: {ex.Message}");
            }

            var textIndexKeys = Builders<Property>.IndexKeys
                .Text(x => x.Name)
                .Text(x => x.Address);

            var textIndexOptions = new CreateIndexOptions { Name = "Name_Address_text" };

            await propertiesCollection.Indexes.CreateOneAsync(
                new CreateIndexModel<Property>(textIndexKeys, textIndexOptions));

            var priceIndex = Builders<Property>.IndexKeys.Ascending(x => x.Price);
            await propertiesCollection.Indexes.CreateOneAsync(
                new CreateIndexModel<Property>(priceIndex, new CreateIndexOptions { Name = "Price_asc" }));

            var ownerIdIndex = Builders<Property>.IndexKeys.Ascending(x => x.OwnerId);
            await propertiesCollection.Indexes.CreateOneAsync(
                new CreateIndexModel<Property>(ownerIdIndex, new CreateIndexOptions { Name = "OwnerId_asc" }));

            Console.WriteLine("Indexes created successfully");
        }
    }
}
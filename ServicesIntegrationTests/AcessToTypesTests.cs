
namespace ServicesIntegrationTests
{
    public class AcessToTypesTests
    {
        const int countOfTypes = 10;

        [Fact]
        public void GetAllAsync_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
            .UseInMemoryDatabase(databaseName: "TestFinanceDB2")
            .Options;

            SetDatabase(countOfTypes, out List<string> types, options);

            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeOfIncome, TypeOfIncomeViewModel>().ReverseMap());

            InAndOutComeEnum expectedIncomeType1 = InAndOutComeEnum.OUTCOME;
            InAndOutComeEnum expectedIncomeType2 = InAndOutComeEnum.INCOME;

            //act
            var acessToTypes = new TypeService(new OperationContext(options), new Mapper(configMapper));

            var actualTypes = acessToTypes.GetAllAsync().Result;

            //assert
            Assert.Equal(countOfTypes, actualTypes.Count);
            Assert.Equal(actualTypes[0].InAndOutCome, expectedIncomeType1);
            Assert.Equal(actualTypes[countOfTypes - 1].InAndOutCome, expectedIncomeType2);
            Assert.Equal(actualTypes.Count, countOfTypes);

        }

        [Fact]
        public void UpdateTypeAsync_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
            .UseInMemoryDatabase(databaseName: "TestFinanceDB2")
            .Options;
            SetDatabase(countOfTypes, out List<string> types, options);
            InAndOutComeEnum expectedInOutCome = InAndOutComeEnum.INCOME;
            var configMapper = new MapperConfiguration(cfg => { 
                cfg.CreateMap<TypeOfIncome, TypeOfIncomeEditDto>().ReverseMap();
                cfg.CreateMap<TypeOfIncome, TypeOfIncomeViewModel>().ReverseMap();
            });
            var expectedType = "testType";

            //act
            var acessToTypes = new TypeService(new OperationContext(options), new Mapper(configMapper));

            TypeOfIncomeEditDto typeOfIncome = new TypeOfIncomeEditDto { InAndOutCome = InAndOutComeEnum.INCOME, Type = expectedType, Id = 1 };

            var isUpdatedSuccessfully = acessToTypes.UpdateTypeAsync(typeOfIncome).Result;
            var firstType = acessToTypes.GetAllAsync().Result.FirstOrDefault(u => u.Id == typeOfIncome.Id);
            bool expetedUpdateSuccessful = true;
            //assert
            Assert.Equal(firstType.InAndOutCome, expectedInOutCome);
            Assert.Equal(firstType.Type, expectedType);
            Assert.Equal(isUpdatedSuccessfully, expetedUpdateSuccessful);
        }

        [Fact]
        public void DeleteTypeOfIncomeAsync_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
            .UseInMemoryDatabase(databaseName: "TestFinanceDB2")
            .Options;
            SetDatabase(countOfTypes, out List<string> types, options);
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeOfIncome, TypeOfIncomeViewModel>().ReverseMap());

            var expetedRequestResult = DeleteRequestResultEnum.SUCCESSFULLY;
            //act
            var context = new OperationContext(options);
            var acessToTypes = new TypeService(context, new Mapper(configMapper));

            var deleteRequestResult = acessToTypes.DeleteTypeAsync(1).Result;

            var listOfTypes = acessToTypes.GetAllAsync().Result;

            //assert
            Assert.Equal(listOfTypes.Count, countOfTypes - 1);
            Assert.Equal(deleteRequestResult, expetedRequestResult);
        }

        private void SetDatabase(int countOfTypes, out List<string> types, DbContextOptions<OperationContext> options)
        {
            types = new List<string>();
            Random rnd = new Random();
            for (int i = 0; i < countOfTypes; i++)
            {
                types.Add(RandomString(12));
            }

            using (var context = new OperationContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                for (int i = 0; i < countOfTypes; i++)
                {
                    if (i > countOfTypes / 2)
                    {
                        context.TypeOfIncomes.Add(new TypeOfIncome { InAndOutCome = InAndOutComeEnum.INCOME, Type = types[i] });
                    }
                    else
                    {
                        context.TypeOfIncomes.Add(new TypeOfIncome { InAndOutCome = InAndOutComeEnum.OUTCOME, Type = types[i] });
                    }

                }
                context.SaveChanges();
                context.Dispose();
            }
        }
        private string RandomString(int l)
        {
            var str = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < l; i++)
            {
                var randomNumber = (byte)random.Next(0, 255);
                str.Append(Convert.ToChar(randomNumber));
            }
            return str.ToString();
        }
    }
}

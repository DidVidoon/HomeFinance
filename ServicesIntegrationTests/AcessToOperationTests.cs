
namespace ServiceIntegrationTests
{
    public class AcessToOperationTests
    {
        const int countOfOperations = 10;

        [Fact]
        public void GetAll_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                .UseInMemoryDatabase(databaseName: "TestFinanceDB")
                .Options;

            SetDatabase(countOfOperations, out List<string> goalsOfOperation, out List<int> incomes, out List<DateTime> dates, options);
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationViewModel>().ReverseMap());

            //act
            var acessToOperation = new OperationService(new OperationContext(options), new Mapper(configMapper));
            var operations = acessToOperation.GetAllAsync(DateTime.MinValue.Date, DateTime.Now).Result;
            int expectedCount = countOfOperations;
            decimal expectedIncome0 = incomes[0];
            decimal expectedIncome1 = incomes[1];

            //assert
            Assert.Equal(operations.Count, expectedCount);
            Assert.Equal(operations[0].Income, expectedIncome0);
            Assert.Equal(operations[1].Income, expectedIncome1);
            Assert.Equal(operations[0].GoalOfOperation, goalsOfOperation[0]);
            Assert.Equal(operations[1].GoalOfOperation, goalsOfOperation[1]);
            Assert.Equal(operations[0].Date, dates[0]);
            Assert.Equal(operations[1].Date, dates[1]);
        }

        [Fact]
        public void GetDailyBalance_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                .UseInMemoryDatabase(databaseName: "TestFinanceDB")
                .Options;
            SetDatabase(countOfOperations, out List<string> goalOfOperation, out List<int> incomes, out List<DateTime> dateTime, options);
            decimal expectedBalance = incomes[0];
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationViewModel>().ReverseMap());

            //act
            var acessToOperation = new OperationService(new OperationContext(options), new Mapper(configMapper));
            DateTime date = new (2022, 03, 01);
            var actualDailyBalance = acessToOperation.GetDailyBalance(date);

            //assert
            Assert.Equal(actualDailyBalance.Result.Balance, expectedBalance);
        }

        [Fact]
        public void GetMounthBalance_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                .UseInMemoryDatabase(databaseName: "TestFinanceDB")
                .Options;
            SetDatabase(countOfOperations, out List<string> goalOfOperation, out List<int> incomes, out List<DateTime> dateTime, options);
            decimal expectedBalance = incomes.Sum();
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationViewModel>().ReverseMap());

            //act
            var acessToOperation = new OperationService(new OperationContext(options), new Mapper(configMapper));
            DateTime firstDate = new (2020, 11, 1);
            DateTime secondDate = new (2022, 03, 10);
            var actualMonthlyBalance = acessToOperation.GetMonthlyBalance(firstDate, secondDate);

            //assert
            Assert.Equal(actualMonthlyBalance.Result.Balance, expectedBalance);
        }

        [Fact]
        public void UpdateOperationSuccessfullyAsync_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                .UseInMemoryDatabase(databaseName: "TestFinanceDb")
                .Options;
            SetDatabase(countOfOperations, out List<string> goalOfOperation, out List<int> incomes, out List<DateTime> dates, options);
            decimal expectedIncome = 100;
            DateTime expectedDate = new DateTime(2022, 03, 10);
            string expectedGoalOfOperation = "testGoal";
            int expectedTypeId = 1;
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationViewModel>().ReverseMap());

            //act
            var acessToOperation = new OperationService(new OperationContext(options), new Mapper(configMapper));
            OperationEditDto operation = new OperationEditDto() { Date = expectedDate, Income = expectedIncome, GoalOfOperation = expectedGoalOfOperation, TypeId = expectedTypeId, Id = 1 };

            var isUpdatedSuccessfully = acessToOperation.UpdateOperationAsync(operation).Result;
            var firstOpertion = acessToOperation.GetAllAsync(DateTime.MinValue.Date, DateTime.Now).Result.FirstOrDefault(u => u.Id == operation.Id);
            bool expectedUpdateSuccessful = true;

            //assert
            Assert.Equal(firstOpertion.Income, expectedIncome);
            Assert.Equal(firstOpertion.Date, expectedDate);
            Assert.Equal(firstOpertion.GoalOfOperation, expectedGoalOfOperation);
            Assert.Equal(firstOpertion.TypeId, expectedTypeId);
            Assert.Equal(isUpdatedSuccessfully, expectedUpdateSuccessful);
        }

        [Fact]
        public void DeleteOperationSuccessfullyAsync_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                .UseInMemoryDatabase(databaseName: "TestFinanceDb")
                .Options;
            SetDatabase(countOfOperations, out List<string> goalOfOperation, out List<int> incomes, out List<DateTime> dates, options);
            bool expectedDeleteSuccessful = true;
            var configMapper = new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationViewModel>().ReverseMap());

            //act
            var acessToOperation = new OperationService(new OperationContext(options), new Mapper(configMapper));
            var isDeletedSuccessfully = acessToOperation.DeleteOperationAsync(1).Result;
            var listOfOperations = acessToOperation.GetAllAsync(DateTime.MinValue.Date, DateTime.Now).Result;

            //assert
            Assert.Equal(listOfOperations.Count, countOfOperations - 1);
            Assert.Equal(isDeletedSuccessfully, expectedDeleteSuccessful);
        }

        [Fact]
        public void Add_Test()
        {
            //arrange
            var options = new DbContextOptionsBuilder<OperationContext>()
                            .UseInMemoryDatabase(databaseName: "TestFinanceDb")
                            .Options;
            SetDatabase(countOfOperations, out List<string> goalOfOperation, out List<int> incomes, out List<DateTime> dates, options);
            decimal expectedIncome = 100;
            DateTime expectedDate = new DateTime(2022, 01, 01);
            string expectedGoalOfOperation = "testGoal";
            int expectedTypeId = 1;
            var configMapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Operation, OperationAddDto>().ReverseMap();
                cfg.CreateMap<Operation, OperationViewModel>().ReverseMap(); 
            });

            //act
            var acessToOperation = new OperationService(new OperationContext(options),  new Mapper(configMapper));
            OperationAddDto operationAddDto = new OperationAddDto { Date = expectedDate, Income = expectedIncome, GoalOfOperation = expectedGoalOfOperation, TypeId = expectedTypeId };
            acessToOperation.AddAsync(operationAddDto);
            var listOfOperations = acessToOperation.GetAllAsync(DateTime.MinValue.Date, DateTime.Now).Result;

            //assert
            Assert.Equal(listOfOperations.Count, countOfOperations+1);
        }

        private void SetDatabase(int countOfOperations, out List<string> goalsOfOperation, out List<int> incomes, out List<DateTime> dates, DbContextOptions<OperationContext> options)
        {
            goalsOfOperation = new List<string>();
            dates = new List<DateTime>();
            incomes = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < countOfOperations; i++)
            {
                goalsOfOperation.Add(RandomString(12));
                dates.Add(new DateTime(2022, 03, i + 1));
                incomes.Add(rnd.Next(-1000, 1000));
            }

            using (var context = new OperationContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                for (int i = 0; i < countOfOperations; i++)
                {
                    context.Operations.Add(new Operation { TypeId = 1, GoalOfOperation = goalsOfOperation[i], Income = incomes[i], Date = dates[i] });
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
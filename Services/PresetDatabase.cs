using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Services
{
    public static class PresetDatabase
    {
        public static void Configurate(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<OperationContext>(Options =>
                     Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly(typeof(OperationContext).Assembly.FullName)));
        }

        public static void Fill(IApplicationBuilder applicationBuilder)
        {
           using(var serviseScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviseScope.ServiceProvider.GetService<OperationContext>();

                context.Database.EnsureCreated();

                if (!context.TypeOfIncomes.Any())
                {
                    context.TypeOfIncomes.AddRange(
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.INCOME,
                            Type = "salary"
                        },
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.INCOME,
                            Type = "dividents"
                        },
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.INCOME,
                            Type = "gift"
                        },
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.OUTCOME,
                            Type = "shopping"
                        },
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.OUTCOME,
                            Type = "taxes"
                        },
                        new TypeOfIncome
                        {
                            InAndOutCome = InAndOutComeEnum.OUTCOME,
                            Type = "utilities payments"
                        }
                        );
                }

                if (!context.Operations.Any())
                {
                    context.Operations.AddRange(
                        new Operation
                        {
                            TypeId = 1,
                            GoalOfOperation = "salary",
                            Income = 2000,
                            Date = new DateTime(2022, 03, 01)
                        },
                        new Operation
                        {
                            TypeId = 4,
                            GoalOfOperation = "bought of IPhone",
                            Income = -3000,
                            Date = new DateTime(2022, 03, 02)
                        },
                        new Operation
                        {
                            TypeId = 2,
                            GoalOfOperation = "dividends",
                            Income = 2000,
                            Date = new DateTime(2022, 04, 03)
                        },
                        new Operation
                        {
                            TypeId = 5,
                            GoalOfOperation = "shopping in supermarket",
                            Income = -400,
                            Date = new DateTime(2022, 04, 04)
                        },
                        new Operation
                        {
                            TypeId = 4,
                            GoalOfOperation = "avto tuning",
                            Income = -2500,
                            Date = new DateTime(2020, 05, 21)
                        },
                        new Operation
                        {
                            TypeId = 3,
                            GoalOfOperation = "Salary",
                            Income = 3000,
                            Date = new DateTime(2022, 05, 25)
                        },
                        new Operation
                        {
                            TypeId = 6,
                            GoalOfOperation = "utilities payments",
                            Income = -500,
                            Date = new DateTime(2022, 05, 27)
                        },
                        new Operation
                        {
                            TypeId = 1,
                            GoalOfOperation = "salary",
                            Income = 1500,
                            Date = new DateTime(2022, 06, 10)
                        }
                        );
                }
                context.SaveChanges();
           }
        }
    }
}

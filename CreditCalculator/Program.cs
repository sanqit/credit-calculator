using System.Text;
using CreditCalculator;
using CreditCalculator.Extensions;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Credit calculator");

while (true)
{
    Console.WriteLine("Какая сумма кредита?");
    var c = double.Parse(Console.ReadLine()!);
    Console.WriteLine("Какая процентная ставка?");
    var percentPerYear = double.Parse(Console.ReadLine()!) / 100;
    Console.WriteLine("На сколько месяцев берёте креди?");
    var s = int.Parse(Console.ReadLine()!);

    Console.WriteLine(
        $"Какой тип расчёт использовать. {CalcType.Annuity}({(int)CalcType.Annuity}) или {CalcType.Differentiated}({(int)CalcType.Differentiated})");

    if (Enum.TryParse<CalcType>(Console.ReadLine()!, true, out var calcType))
    {
        var creditCalculatorFactory = new CreditCalculatorFactory();
        var calculator = creditCalculatorFactory.CreateCalculator(calcType);

        var calculationResult = calculator.Calculate(new CalculationParameters(
            c,
            percentPerYear,
            s
        ));

        calculationResult.Print();
    }
    else
    {
        Console.WriteLine("Не корректный тип расчёта.");
    }

    Console.WriteLine("Выйти из программы? y/n");
    var exitCommand = Console.ReadLine()!;
    if (string.Equals(exitCommand, "y", StringComparison.InvariantCultureIgnoreCase))
    {
        break;
    }
}
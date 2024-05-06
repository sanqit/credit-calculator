using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Credit calculator");

Console.WriteLine("Какая сумма кредита?");
var c = double.Parse(Console.ReadLine()!);
Console.WriteLine("Какая процентная ставка?");
var percentPerYear = double.Parse(Console.ReadLine()!) / 100;
Console.WriteLine("На сколько месяцев берёте креди?");
var s = int.Parse(Console.ReadLine()!);

var m = percentPerYear / 12;
var k = m * Math.Pow(1 + m, s) / (Math.Pow(1 + m, s) - 1);
var x = c * k;

var payment = Math.Round(x, 2);
var totalPayment = Math.Round(payment * s, 2);
var totalPercent = Math.Round(totalPayment - c, 2);

Console.WriteLine($"Ежемесячный платёж {payment:0.00}");
Console.WriteLine($"Начисленные процены {totalPercent:0.00}");
Console.WriteLine($"Долг + проценты {totalPayment:0.00}");

var mainDebtInPercent = Math.Round(c / totalPayment, 2) * 100;
var percentsInPercent = 100 - mainDebtInPercent;

Console.WriteLine($"От общей уплаченной суммы: Основной длог={mainDebtInPercent:0.00}%;Проценты={percentsInPercent:0.00}%");

var debt = c;

var paymentNumber = 0;
var sumPayment = 0d;
var sumMainDebtPayment = 0d;
var sumPercentPayment = 0d;

var paymentInfos = new List<PaymentInfo>();
while (debt > 0)
{
    paymentNumber++;
    var percentPayment = Math.Round(debt * m, 2);
    var mainDebtPayment = Math.Round(payment - percentPayment, 2);

    if (debt < mainDebtPayment)
    {
        mainDebtPayment = debt;
        percentPayment = 0;
        payment = mainDebtPayment;
    }

    debt = Math.Round(debt - mainDebtPayment, 2);
    var paymentInfo = new PaymentInfo(
        paymentNumber,
        payment,
        mainDebtPayment,
        percentPayment,
        debt);
    paymentInfos.Add(paymentInfo);

    sumPayment += payment;
    sumMainDebtPayment += mainDebtPayment;
    sumPercentPayment += percentPayment;
}

Console.WriteLine($"№\tСумма платежа\tПлатёж по основному долгу\tПлатёж по процентам\tОстаток долга");
foreach (var paymentInfo in paymentInfos)
{
    Console.WriteLine($"{paymentInfo.PaymentNumber}\t{paymentInfo.Payment:0.00}\t{paymentInfo.MainDebtPayment:0.00}\t\t\t{paymentInfo.PercentPayment:0.00}\t\t\t{paymentInfo.Debt:0.00}");
}

Console.WriteLine($"Выплачено всего: {sumPayment:0.00}");
Console.WriteLine($"Сумма выплаченного долга:{sumMainDebtPayment:0.00}");
Console.WriteLine($"Сумма выплаченных процентов:{sumPercentPayment:0.00}");

foreach (var paymentInfo in paymentInfos)
{
    PrintDebtAndPercents(paymentInfo.MainDebtPayment, paymentInfo.PercentPayment);
}

Console.ReadKey();

return;

static void PrintDebtAndPercents(double left, double right)
{
    var total = left + right;

    var color = Console.ForegroundColor;

    var countLeft = (int)(left / total * 100);
    var countRight = 100 - countLeft;
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write(new string('#', countLeft));
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(new string('#', countRight));
    Console.ForegroundColor = color;
}

internal record PaymentInfo(
    int PaymentNumber,
    double Payment,
    double MainDebtPayment,
    double PercentPayment,
    double Debt
);
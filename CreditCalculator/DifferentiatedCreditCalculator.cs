namespace CreditCalculator;

internal class DifferentiatedCreditCalculator : ICreditCalculator
{
    public CalculationResult Calculate(
        CalculationParameters parameters
    )
    {
        //https://finuslugi.ru/glossariy/raschyot_differencirovannogo_platezha
        var m = parameters.PercentPerYear / 12;
        var mainDebtPayment = Math.Round(parameters.CreditSum / parameters.PeriodsCount, 2);

        var debt = parameters.CreditSum;

        var paymentNumber = 0;
        var sumPayment = 0d;
        var sumMainDebtPayment = 0d;
        var sumPercentPayment = 0d;

        var paymentInfos = new List<PaymentInfo>();
        while (debt > 0)
        {
            paymentNumber++;
            var percentPayment = Math.Round(debt * m, 2);

            var paymentForCalculation = percentPayment + mainDebtPayment;

            debt = Math.Round(debt - mainDebtPayment, 2);
            var paymentInfo = new PaymentInfo(
                paymentNumber,
                paymentForCalculation,
                mainDebtPayment,
                percentPayment,
                debt
            );

            paymentInfos.Add(paymentInfo);

            sumPayment += paymentForCalculation;
            sumMainDebtPayment += mainDebtPayment;
            sumPercentPayment += percentPayment;
        }

        var mainDebtInPercent = Math.Round(parameters.CreditSum / sumPayment, 2) * 100;
        var percentsInPercent = 100 - mainDebtInPercent;

        return new CalculationResult(
            null,
            sumPayment,
            sumMainDebtPayment,
            sumPercentPayment,
            mainDebtInPercent,
            percentsInPercent,
            paymentInfos
        );
    }
}
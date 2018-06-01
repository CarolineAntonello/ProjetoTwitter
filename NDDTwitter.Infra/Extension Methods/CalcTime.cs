using System;

namespace NDDTwitter.Infra
{
    public class CalcTime
    {
        public string CalculateTime(DateTime date)
        {
            TimeSpan time = DateTime.Now - date;

            //Cálculo anos
            if (time.Days >= 365)
            {
                int years = time.Days / 365;

                if (years >= 2)
                    return string.Format("{0} anos atrás", years);
                if (years == 1)
                    return string.Format("{0} ano atrás", years);
            }

            //Cálculo meses
            if (time.Days >= 30)
            {
                int months = time.Days / 30;

                if (months >= 2)
                    return string.Format("{0} meses atrás", months);
                if (months == 1)
                    return string.Format("{0} mês atrás", months);
            }
            //Cálculo semanas
            if (time.Days >= 7)
            {
                int weeks = time.Days / 7;

                if (weeks >= 2)
                    return string.Format("{0} semanas atrás", weeks);
                if (weeks == 1)
                    return string.Format("{0} semana atrás", weeks);
            }
            //Cálculo Dias
            if (time.Days > 1)
                return string.Format("{0} dias atrás", time.Days);
            if (time.Days == 1)
                return string.Format("{0} dia atrás", time.Days);
            //Cálculo Horas
            if (time.Hours > 1)
                return string.Format("{0} Horas atrás", time.Hours);
            if (time.Hours == 1)
                return string.Format("{0} Hora atrás", time.Hours);
            //Cálculo Minutos
            if (time.Minutes > 1)
                return string.Format("{0} minutos atrás", time.Minutes);
            else
                return "1 minuto atrás";

        }
    }
}

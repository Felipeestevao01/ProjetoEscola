using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal class Utilitarios
    {
        public static DateTime DataHoraStringParaDateTime(string data)
        {
            string[] dataEHora = data.Split('T');
            string[] componentesData = dataEHora[0].Split("-");
            int ano = int.Parse(componentesData[0]);
            int mes = int.Parse(componentesData[1]);
            int dia = int.Parse(componentesData[2]);
            string[] componentesHora = dataEHora[1].Split(":");
            int horas = int.Parse(componentesHora[0]);
            int minutos = int.Parse(componentesHora[1]);
            int segundos = int.Parse(componentesHora[2]);
            DateTime dataConvertida = new(ano, mes, dia, horas, minutos, segundos);
            return dataConvertida;
        }
    }
}

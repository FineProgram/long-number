using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace long_number
{
    class Long_number {
        private string undo_point = "";
        private string post_point = "";
        private bool sign=false;//положительное если есть знак
        public string setStr (string LN)
        {
            string[] Temp_array = LN.Split('.');
            undo_point = Temp_array[0];
            if (undo_point[0] == '-') { sign = true;  undo_point.Remove(0); }//записываем наличие знака
            if (Temp_array.Length == 2)//если точка есть, то у нас будет 2 массива
            {
                post_point = Temp_array[1];
            }
            return LN;
        }//будет у нас конструктором
        public Long_number(string LN)
        {
            setStr(LN);
        }//
        public string getString()
        {
            if (string.IsNullOrEmpty(post_point))
            {
                return "" + undo_point;
            }
            else return "" + undo_point + "." + post_point;
        }
        public static string operator + (Long_number A, Long_number B)//cложение
        {
            #region вспомогательные переменные
            string Aup = A.undo_point;//части чисел до и после точки
            string Bup = B.undo_point;
            string App = A.post_point;
            string Bpp = B.post_point;
            string resultpp="";//то же для результата
            string resultup="";
            int temp=0;
            #endregion
            #region здесь мы будем приписывать нули
            while (A.undo_point.Length > B.undo_point.Length) Bup = "0" + Bup;
            while (A.undo_point.Length < B.undo_point.Length) Aup = "0" + Aup;
            while (A.post_point.Length > B.post_point.Length) Bup = Bup + "0";
            while (A.post_point.Length < B.post_point.Length) Aup = Aup + "0";
            #endregion
            #region сложение после точки 
            for (int i = App.Length - 1; i >= 0; i--)
            {
                temp = temp + (int)char.GetNumericValue(App[i]) + (int)char.GetNumericValue(Bpp[i]);
                resultpp = (temp % 10) + resultpp;
                temp = temp / 10;
            }
            #endregion
            #region сложение до точки
            for (int i= Aup.Length-1; i >=0 ; i--)
            {
                temp = temp + (int)char.GetNumericValue(Aup[i]) + (int)char.GetNumericValue(Bup[i]);
                resultup = (temp % 10) + resultup;
                temp = temp / 10;
            }
            if (temp > 0) resultpp = temp + resultup;//грациознее пререносить последнюю единицу не придумал
            #endregion
            #region вывод
            if (!(string.IsNullOrEmpty(resultpp)))//обрезаем лишние нули тут или выводим ответ если хвоста нет
            {
                while (resultpp.EndsWith("0")) resultpp = resultpp.Remove(resultpp.Length - 1);
                if (string.IsNullOrEmpty(resultpp)) { return resultup; }
            }
            else return resultup;//собственно, вывод
            return "" + resultup + "." + resultpp;//вывод когда хвост есть
            #endregion
        }
        public static string operator -(Long_number A, Long_number B)//вычетание
        {
            #region вспомогательные переменные, как и в сложении
            string Aup = A.undo_point;//части чисел до и после точки
            string Bup = B.undo_point;
            string App = A.post_point;
            string Bpp = B.post_point;
            string resultpp = "";//то же для результата
            string resultup = "";
            int i, temp = 0;
            bool sign = false;
            #endregion
            #region здесь мы будем приписывать нули, угадай окуда скопипастил
            while (A.undo_point.Length > B.undo_point.Length) Bup = "0" + Bup;
            while (A.undo_point.Length < B.undo_point.Length) Aup = "0" + Aup;
            while (A.post_point.Length > B.post_point.Length) Bup = Bup + "0";
            while (A.post_point.Length < B.post_point.Length) Aup = Aup + "0";
            #endregion
            #region теперь будем вычетать
            i = 0;
           if (A.sign) { }//здесь можно весело убежать к сложению, впоследствии просто приписав знак
            if ((Aup[0] - Bup[0] < 0)) { sign = true; }//тут надо придумать как считать число ставшим отрицательным и оставшиеся положительным
            for (i = 0; i <= App.Length - 1; i++)
            {
                temp = temp + (int)char.GetNumericValue(App[i]) - (int)char.GetNumericValue(Bpp[i]);
                resultpp = resultpp + (temp % 10);
                temp = temp / 10;
            }
            #endregion
            return "";
        }
    }   
    class Program
    {
        static void Main(string[] args)
        {
            Long_number A = new Long_number(Console.ReadLine());
            Long_number B = new Long_number(Console.ReadLine());
            Console.WriteLine(A + B);
            Console.WriteLine(A - B);
            Console.ReadLine();
        }
    }
}

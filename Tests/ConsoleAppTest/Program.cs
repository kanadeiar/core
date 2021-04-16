﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppTest
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleRussian();
            Console.WriteLine("Асинхронное программирование\n\n");

            Console.WriteLine(DecodeMasa("104102098102111101102115117059033051051047049054047051049050058033050051059050054059051058"));
            Console.WriteLine(DecodeMasa("046046046046046046046046046046046046046046046046046046046046046046046046046046046046046046"));
            Console.WriteLine();
            Console.WriteLine(DecodeMasa("048048046063069098117118110033089033071115102106116100105098109117033068112101102059053055057051"));
            Console.WriteLine(DecodeMasa("119098109118102050033062033051049050052049056050055060"));
            Console.WriteLine(DecodeMasa("119098109118102051033062033049058051051060"));
            Console.WriteLine();
            Console.WriteLine(DecodeMasa("116102115106098109033059062033084033068046076052083088054057054053051049050057060033033033033033033033033033048048084102115106102111111118110110102115033101102115033068081086033033033033033033"));
            Console.WriteLine();
            Console.WriteLine(DecodeMasa("101106115102100117096111098119059062050060033033033033033033033033033033033033033033033033033033033033033033048048099112112109033111102118102033079098119106104098117106112111033101118115100105033067106109101102115033033033033033033033033"));
            Console.WriteLine(DecodeMasa("111102120066067099117111116059062050060033033033033033033033033033033033033033033033033033033033033033033033048048111102118102033067118117117112111116033103253115033066067116033033033033033033"));
            Console.WriteLine(DecodeMasa("111102120066067099117111116081112116059062050060033033033033033033033033033033033033033033033033033033033033048048111102118102033081112116106117106112111106102115118111104033101102115033066067033067118117117112111116033033033033033033"));
            Console.WriteLine(DecodeMasa("110102115104102081106100116033059062050060033"));
            Console.WriteLine();
            Console.WriteLine(DecodeMasa("048048046063087102115099106111101118111104"));
            Console.WriteLine(DecodeMasa("108112113113109118111104033062033054060033033033033033033033033033033033033033033033033033033033033033033033048048033049059033112103103109106111102060033054059033084085070081054033116102115106102109109060033056059033084085070081056033118102099102115033111102117123120102115108060033051054054059033112103103109106111102033110106117033123118103098109109116120102115117102111033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033"));
            Console.WriteLine(DecodeMasa("106113046098101115102116116102033062033050049047049047054056047050049060033033033033033033033033033033033033048048033080076033033033033033033"));
            Console.WriteLine(DecodeMasa("116113116051033062033049060033033033033033033033033033033033033033033033033033033033033033033033033033033033048048099112112109033050062117115118102033033033049062103098109116102033125033110106117033051047033116113116033108112110110118111106123106102115102111060033116112111116117059033108102106111102033051047033116113116033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033"));
            Console.WriteLine(DecodeMasa("106113046098101115102116116102051033062033050049047050054047052047051060033033033033033033033033033033033033048048033106113046098101115102116116102062051049047051047051051047051060033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033"));
            Console.WriteLine(DecodeMasa("113100046111115033062033050060033033033033033033033033033033033033033033033033033033033033033033033033033033048048033050059033110106116100105098111109098104102033044033116100105109098110110109098104102115118111104060033051059033104098102115099118102105111102033044033104106102116116099118102105111102033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033033"));

            Console.WriteLine("\nНажмите любую кнопку ...");
            Console.ReadKey();
        }

        private static string DecodeMasa(string str)
        {
            var sb = new StringBuilder();
            var len = str.Length / 3;
            for (int i = 0; i < len; i++)
            {
                var val = int.Parse(str.Substring(i * 3, 3)) - 1;
                sb.Append(Convert.ToChar(val));
            }

            return sb.ToString();
        }

        private static void ConsoleRussian()
        {
            [DllImport("kernel32.dll")] static extern bool SetConsoleCP(uint pagenum);
            [DllImport("kernel32.dll")] static extern bool SetConsoleOutputCP(uint pagenum);
            SetConsoleCP(65001);        //установка кодовой страницы utf-8 (Unicode) для вводного потока
            SetConsoleOutputCP(65001);  //установка кодовой страницы utf-8 (Unicode) для выводного потока
        }
    }
}

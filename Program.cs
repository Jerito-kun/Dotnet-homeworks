using System;
using System.Collections.Generic;

/*
Ступенчатый массив из двумерных массивов

Цель:
Создать ступенчатый массив из двумерных массивов со списками всех возможных ходов шахматных фигур.

Создать перечисление для шахматных фигур: ладья, конь, слон, ферзь, король.
Создать структуру для хранения координат полей шахматной доски (x, y).
Создать ступенчатый массив:
первый аргумент - фигура.
второй аргумент - матрица из двух координат (x, y) полей шахматной доски.

Сгенерировать список всех возможных ходов указанной шахматной фигуры
с указанного поля, поместить эти координаты в список и
сохранить в соответствующем элементе массива.

Используя сгенерированный массив, написать код для ответа на вопросы:

Сколько всего возможных ходов у всех фигур со всех полей?
Сколько всего возможных ходов на поле а1, координата (0, 0)
Выписать координаты для каждой фигуры, с которого меньше всего возможных ходов.
Сгенерировать c# код с непосредственным описанием элементов массива.
*/

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Создать ступенчатый массив:
            первый аргумент - фигура.
            второй аргумент - матрица из двух координат (x, y) полей шахматной доски.*/

            List<ChessboardCoordinates>[][] CheckMateMovies = new List<ChessboardCoordinates>[Enum.GetValues(typeof(ChessFigures)).Length][];  //  всего 5 фигур
            int summaryMoves = 0;

            /*Сгенерировать список всех возможных ходов указанной шахматной фигуры
            с указанного поля, поместить эти координаты в список 
            и сохранить в соответствующем элементе массива.*/

            foreach (var fig in Enum.GetValues(typeof(ChessFigures)))
            {
                CheckMateMovies[(int)fig] = new List<ChessboardCoordinates>[64];  //  для каждой из 64 клеток доски
                for (int i = 0; i < CheckMateMovies[(int)fig].Length; i++)
                {
                    CheckMateMovies[(int)fig][i] = GetMoviesList((ChessFigures)fig, ArrayIdToCoordinates(i));
                    summaryMoves += CheckMateMovies[(int)fig][i].Count;
                }
            }

            //  Сколько всего возможных ходов у всех фигур со всех полей?

            Console.WriteLine($"Total moves: {summaryMoves}");

            //  Сколько всего возможных ходов на поле а1, координата (0, 0)

            summaryMoves = 0;
            foreach (var item in CheckMateMovies)
            {
                summaryMoves += item[0].Count;
            }
            Console.WriteLine($"Total moves at A1 field: {summaryMoves}");

            //  Выписать координаты для каждой фигуры, с которого меньше всего возможных ходов.

            ChessboardCoordinates minimalMovieCoordinate = new ChessboardCoordinates();
            int minimalMovieCount;
            foreach (var fig in Enum.GetValues(typeof(ChessFigures)))
            {
                minimalMovieCount = 64;  //  На самом деле, для всех фигур будет возвращена клетка A1
                for (int i = 0; i < CheckMateMovies[(int)fig].Length; i++)
                {
                    if ((CheckMateMovies[(int)fig][i]).Count < minimalMovieCount)
                    {
                        minimalMovieCount = (CheckMateMovies[(int)fig][i]).Count;
                        minimalMovieCoordinate = ArrayIdToCoordinates(i);
                    }
                }
                Console.WriteLine($"Minimal moves for {(ChessFigures)fig} is {minimalMovieCount} at field {minimalMovieCoordinate.coordinateX}{minimalMovieCoordinate.coordinateY}");
            }

            ChessboardCoordinates maximalMovieCoordinate = new ChessboardCoordinates();
            int maximalMovieCount;
            foreach (var fig in Enum.GetValues(typeof(ChessFigures)))
            {
                maximalMovieCount = 0;  //  В задании этого не было, но более наглядно работает сравнение
                for (int i = 0; i < CheckMateMovies[(int)fig].Length; i++)
                {
                    if ((CheckMateMovies[(int)fig][i]).Count > maximalMovieCount)
                    {
                        maximalMovieCount = (CheckMateMovies[(int)fig][i]).Count;
                        maximalMovieCoordinate = ArrayIdToCoordinates(i);
                    }
                }
                Console.WriteLine($"Maximal moves for {(ChessFigures)fig} is {maximalMovieCount} at field {maximalMovieCoordinate.coordinateX}{maximalMovieCoordinate.coordinateY}");
            }
        }
        /*Создать перечисление для шахматных фигур: ладья, конь, слон, ферзь, король.*/
        enum ChessFigures
        {
            Rook = 0,  //  ладья
            Knight = 1,  //  конь
            Bishop = 2,  //  слон
            Queen = 3,  //  ферзь
            King = 4 //  король
        }
        /*Создать структуру для хранения координат полей шахматной доски (x, y).*/
        struct ChessboardCoordinates
        {
            public char coordinateX;  //  буквы латинского алфавита от A до H
            public int coordinateY;  //  арабские цифры от 1 до 8
        }
        static List<ChessboardCoordinates> GetMoviesList(ChessFigures figureName, ChessboardCoordinates initialCoordinates)
        {
            string s = ($"{figureName} at {initialCoordinates.coordinateX}{initialCoordinates.coordinateY}: ");
            var coordinateX = LetterToNumber(initialCoordinates.coordinateX);
            var coordinateY = initialCoordinates.coordinateY;
            ChessboardCoordinates coords;
            List<ChessboardCoordinates> output = new List<ChessboardCoordinates>();
            switch (figureName)
            {
                case ChessFigures.Rook:  //  ладья движется по всем полям по горизонтали и вертикали от исходного
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (i != coordinateX)
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(i); coords.coordinateY = coordinateY;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(i)}{coordinateY}");
                            }
                        }
                        for (int j = 1; j <= 8; j++)
                        {
                            if (j != coordinateY)
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(coordinateX); coords.coordinateY = j;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(coordinateX)}{j}");
                            }
                        }
                        break;
                    }
                case ChessFigures.Knight:  //  конь перемещается на любую свободную клетку, 
                                           //  отстоящую от исходной на три клетки по одному направлению и две клетки по следующему  (только вертикально и горизонтально)
                    {
                        for (int i = coordinateX - 2; i <= coordinateX + 2; i = i + 4)
                        {
                            for (int j = coordinateY - 1; j <= coordinateY + 1; j = j + 2)
                            {
                                if ((i > 0) && (i < 9) && (j > 0) && (j < 9))
                                {
                                    coords = new ChessboardCoordinates();
                                    coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                    output.Add(coords);
                                    //Console.WriteLine($"{NumberToLetter(i)}{j}");
                                }
                            }
                        }
                        for (int i = coordinateX - 1; i <= coordinateX + 1; i = i + 2)
                        {
                            for (int j = coordinateY - 2; j <= coordinateY + 2; j = j + 4)
                            {
                                if ((i > 0) && (i < 9) && (j > 0) && (j < 9))
                                {
                                    coords = new ChessboardCoordinates();
                                    coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                    output.Add(coords);
                                    //Console.WriteLine($"{NumberToLetter(i)}{j}");
                                }
                            }
                        }
                        break;
                    }
                case ChessFigures.Bishop:  //  слон движется по диагоналям во всех направлениях от исходного поля
                    {
                        var i = coordinateX + 1;
                        var j = coordinateY + 1;
                        while ((i != coordinateX) && (j != coordinateY))
                        {
                            if ((i > 8) || (j > 8))
                            {
                                i = i - 9;
                                j = j - 9;
                            }
                            if ((i > 0) && (j > 0))
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(i)}{j}");
                            }
                            i++;
                            j++;
                        }

                        i = coordinateX + 1;
                        j = coordinateY - 1;
                        while ((i != coordinateX) && (j != coordinateY))
                        {
                            if ((i > 8) || (j < 1))
                            {
                                i = i - 9;
                                j = j + 9;
                            }
                            if ((i > 0) && (j < 9))
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(i)}{j}");
                            }
                            i++;
                            j--;
                        }
                        break;
                    }
                case ChessFigures.Queen:  //  ферзь движется в любом направлении по горизонтали/вертикали/дмагонали от исходной клетки
                    {
                        for (int k = 1; k <= 8; k++)
                        {
                            if (k != coordinateX)
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(k); coords.coordinateY = coordinateY;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(k)}{coordinateY}");
                            }
                        }
                        for (int m = 1; m <= 8; m++)
                        {
                            if (m != coordinateY)
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(coordinateX); coords.coordinateY = m;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(coordinateX)}{m}");
                            }
                        }

                        var i = coordinateX + 1;
                        var j = coordinateY + 1;
                        while ((i != coordinateX) && (j != coordinateY))
                        {
                            if ((i > 8) || (j > 8))
                            {
                                i = i - 9;
                                j = j - 9;
                            }
                            if ((i > 0) && (j > 0))
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(i)}{j}");
                            }
                            i++;
                            j++;
                        }

                        i = coordinateX + 1;
                        j = coordinateY - 1;
                        while ((i != coordinateX) && (j != coordinateY))
                        {
                            if ((i > 8) || (j < 1))
                            {
                                i = i - 9;
                                j = j + 9;
                            }
                            if ((i > 0) && (j < 9))
                            {
                                coords = new ChessboardCoordinates();
                                coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                output.Add(coords);
                                //Console.WriteLine($"{NumberToLetter(i)}{j}");
                            }
                            i++;
                            j--;
                        }
                        break;
                    }
                case ChessFigures.King:  //  король движется на одну клетку в любом направлении по горизонтали/вертикали/дмагонали от исходной
                    {
                        for (int i = coordinateX - 1; i < coordinateX + 2; i++)
                        {
                            for (int j = coordinateY - 1; j < coordinateY + 2; j++)
                            {
                                if ((i > 0) && (i < 9) && (j > 0) && (j < 9))
                                {
                                    if (!((i == coordinateX) && (j == coordinateY)))
                                    {
                                        coords = new ChessboardCoordinates();
                                        coords.coordinateX = NumberToLetter(i); coords.coordinateY = j;
                                        output.Add(coords);
                                        //Console.WriteLine($"{NumberToLetter(i)}{j}");
                                    }
                                }
                            }
                        }
                        break;
                    }
                default:
                    throw new Exception("Figure can not be recognized");
            }
            s += $"<{output.Count}> ";
            foreach (var item in output)
            {
                s += ($"{item.coordinateX}{item.coordinateY}, ");
            }
            Console.WriteLine($"[INFO] {s.Substring(0, s.Length - 2)}");
            return output;
        }
        static int LetterToNumber(char letter)
        {
            int output = 0;
            try
            {
                output = (int)letter;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error calling LetterToNumber with {letter}, {ex.Message}");
                return output;
            }
            output -= 64;
            //Console.WriteLine(output);
            return output;
        }
        static char NumberToLetter(int number)
        {
            return (char)(number + 64);
        }
        static int CoordinatesToArrayId(ChessboardCoordinates coordinates)
        {
            return LetterToNumber(coordinates.coordinateX) * coordinates.coordinateY - 1;
        }
        static ChessboardCoordinates ArrayIdToCoordinates(int arrayId)
        {
            ChessboardCoordinates coordinates = new ChessboardCoordinates();
            coordinates.coordinateX = NumberToLetter(arrayId / 8 + 1);
            coordinates.coordinateY = arrayId % 8 + 1;
            return coordinates;
        }
    }

}



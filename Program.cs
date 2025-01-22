using CSharp_lab_4;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

class Program
{
    public static void Main()
    {
        //Exercise1();
        //Console.WriteLine();

        //Exercise2();
        //Console.WriteLine();

        //Exercise3();
        //Console.WriteLine();

        //Exercise4();
        //Console.WriteLine();

        Exercise5();
        Console.WriteLine();
    }

    // Задание 1
    public static void Exercise1()
    {
        Console.WriteLine("Задание 1.");

        // Исходные списки
        var listInt = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 6, 2, 3, 4 };
        var listDouble = new List<double> { 1.1, 1.2, 3.4, 5.6, 5.6, 2.3, 4.5, 1.5 };
        var listChar = new List<char> { 'a', 'b', 'c', 'r', 'a', 'c', 'b', 'a' };
        var listString = new List<string> { "look at", "confirm", "sorry", "confirm", "legacy" };

        // Вывод исходных списков
        Console.Write("Исходный список целых чисел: ");
        PrintList(listInt);
        Console.Write("Исходный список вещественных чисел: ");
        PrintList(listDouble);
        Console.Write("Исходный список символов: ");
        PrintList(listChar);
        Console.Write("Исходный список строк: ");
        PrintList(listString);
        Console.WriteLine();

        // Элементы для повторения
        int elementInt = 5;
        double elementDouble = 5.6;
        char elementChar = 'a';
        string elementString = "confirm";

        // Использование метода
        RepeatElement(elementInt, listInt);
        RepeatElement(elementDouble, listDouble);
        RepeatElement(elementChar, listChar);
        RepeatElement(elementString, listString);

        // Вывод изменённых списков
        Console.Write("Полученный список целых чисел: ");
        PrintList(listInt); 
        Console.Write("Полученный список вещественных чисел: ");
        PrintList(listDouble);
        Console.Write("Полученный список символов: ");
        PrintList(listChar);
        Console.Write("Полученный список строк: ");
        PrintList(listString);
    }

    // Повтор дважды каждого вхождения элемента Е в список L
    private static void RepeatElement<T>(T E, List<T> L)
    {
        for (int i = 0; i < L.Count; i++)    // Проходимся по всему списку
        {
            if (L[i].Equals(E))      // Если значения равны
            {
                L.Insert(i + 1, E);  // Вставляем элемент в список
                i++;                 // Перескакиваем на следующий элемент
            }
        }
    }

    // Вывод списка
    private static void PrintList<T>(List<T> list) 
    {
        foreach (T item in list) 
        {
            Console.Write(item + "\t");
        }
        Console.WriteLine();
    }


    // Задание 2
    public static void Exercise2()
    {
        Console.WriteLine("Задание 2.");

        // Создание списков
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 5, 3, 2, 1 };
        var list1 = new List<double> { 1.2, 3.4, 5.6, 7.8, -6.7, -8.9, 10.3, 11.5, 9.5, -2.4 };
        var list2 = new List<char> { 'a', 'b', 'c', 'd', 'a', 'b', 'c', 'd' };
        var list3 = new List<string> { "good guy", "bad guy", "new", "older", "sorry" };

        LinkedList<int> numbers = new(list);
        LinkedList<double> doubles = new(list1);
        LinkedList<char> chars = new(list2);
        LinkedList<string> strings = new(list3);

        // Вывод исходных списков
        Console.Write("Исходный список целых чисел: ");
        PrintLinkedList(numbers);
        Console.Write("Исходный список вещественных чисел: ");
        PrintLinkedList(doubles);
        Console.Write("Исходный список символов: ");
        PrintLinkedList(chars);
        Console.Write("Исходный список строк: ");
        PrintLinkedList(strings);
        Console.WriteLine();

        // Применение метода
        ChangeBetweenMaxAndMin(numbers);
        ChangeBetweenMaxAndMin(doubles);
        ChangeBetweenMaxAndMin(chars);
        ChangeBetweenMaxAndMin(strings);

        // Вывод изменённых списков
        Console.Write("Полученный список целых чисел: ");
        PrintLinkedList(numbers);
        Console.Write("Полученный список вещественных чисел: ");
        PrintLinkedList(doubles);
        Console.Write("Полученный список символов: ");
        PrintLinkedList(chars);
        Console.Write("Полученный список строк: ");
        PrintLinkedList(strings);
    }

    // Поменять местами фрагменты между минимальными и максимальными элементами
    private static void ChangeBetweenMaxAndMin<T>(LinkedList<T> list) where T : IComparable<T>
    {
        // Если в списке меньше двух элементов
        if (list.Count < 2) return;

        // Находим минимальный и максимальный элементы
        var maxNode = list.First;
        var minNode = list.First;

        var curNode = list.First;
        while (curNode != null) 
        { 
            if (curNode.Value.CompareTo(minNode.Value) < 0)
            {
                minNode = curNode;
            }
            if (curNode.Value.CompareTo(maxNode.Value) > 0)
            {
                maxNode = curNode;
            }

            curNode = curNode.Next;
        }

        // Проверка, что элементы находятся в допустимой позиции для замены
        if (minNode == maxNode || minNode.Next == maxNode || maxNode.Next == minNode)
            return;

        if (minNode.Next == maxNode || maxNode.Next == minNode)
            return;

        if (list.Find(minNode.Value) == null || list.Find(maxNode.Value) == null)
            return;

        if (list.Find(minNode.Value) == list.Last || list.Find(maxNode.Value) == list.Last)
            return;

        // Определение границ сегмента для перестановки
        LinkedListNode<T> start = minNode.Next;
        LinkedListNode<T> end = maxNode.Previous;
        if (minNode.Value.CompareTo(maxNode.Value) > 0)
        {
            start = maxNode.Next;
            end = minNode.Previous;
        }

        // Используем стек для обращения порядка элементов
        Stack<T> segment = new Stack<T>();
        curNode = start;
        while (curNode != end.Next)
        {
            segment.Push(curNode.Value); // Добавляем элементы в стек
            curNode = curNode.Next;
        }

        // Замена элементов в обратном порядке
        curNode = start;
        while (curNode != end.Next)
        {
            curNode.Value = segment.Pop(); // Извлекаем элементы из стека и записываем обратно в список
            curNode = curNode.Next;
        }
    }

    // Вывод LinkedList
    private static void PrintLinkedList<T>(LinkedList<T> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + "\t");
        }
        Console.WriteLine();
    }


    // Задание 3
    public static void Exercise3()
    {
        Console.WriteLine("Задание 3.");

        var parties = new HashSet<string>{
            "Воля", "Народ", "Мир", "Надежда", "Русь" };

        Console.Write("Список партий: ");
        foreach (var party in parties)
        {
            Console.Write(party + ", ");
        }
        Console.WriteLine();

        Console.Write("Введите количество голосующих групп: ");
        uint n;  // Количество групп голосующих
        try
        {
            n = Convert.ToUInt32(Console.ReadLine());
            if (n == 0) throw new Exception();
        }
        catch
        {
            Console.WriteLine("Неправильно введены данные");
            return;
        }

        var groups = new List<HashSet<string>>();
        for (int i = 0; i < n; i++)
        {
            groups.Add(new HashSet<string>());

            Console.Write($"За сколько партий голосует группа {i + 1}: ");
            uint count;
            try
            {
                count = Convert.ToUInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Неправильно введено число");
                return;
            }
            for (int j = 0; j < count; j++)
            {
                string party;
                Console.Write("Введите название партии для голоса: ");
                try
                {
                    party = Console.ReadLine();
                    if (!parties.Contains(party)) throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Такой партии нет в списке");
                    return;
                }

                groups[i].Add(party);
            }
        }

        Console.WriteLine();

        // Вывод за какие партии проголосовали граждане из каждой группы
        for (int i = 0; i < groups.Count; i++) 
        {
            Console.WriteLine($"Группа {i + 1} проголосовала за {string.Join(", ", groups[i])}");
        }

        // Вывод за какие партии проголосовали граждане только из одной группы
        var uniqueVotes = new HashSet<string>();
        foreach (var group in groups)
        {
            foreach (var party in group)
            {
                // Партия должна быть только в одной группе
                if (groups.Count(g => g.Contains(party)) == 1)
                {
                    uniqueVotes.Add(party);
                }
            }
        }
        Console.WriteLine("\nПартии, за которые проголосовали только в одной группе:");
        // Вывод списка партий, за которые голосовали только в одной группе
        Console.WriteLine(uniqueVotes.Count > 0 ? string.Join(", ", uniqueVotes) : "Нет таких партий.");

        // Поиск партий, за которые не проголосовали
        var allVotedParties = new HashSet<string>();
        foreach (var group in groups)
        {
            // Объединяем все голоса всех групп
            allVotedParties.UnionWith(group);
        }
        var notVotedParties = new HashSet<string>(parties.Except(allVotedParties));

        // Вывод партий, оставшихся без голосов
        Console.WriteLine("\nПартии, за которые не проголосовали:");
        Console.WriteLine(notVotedParties.Count > 0 ? string.Join(", ", notVotedParties) : "Все партии получили голоса.");
    }


    // Задание 4
    public static void Exercise4()
    {
        Console.WriteLine("Задание 4.");

        // Путь к файлу с текстом
        string filePath = "text.txt";

        // Проверяем существование файла
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        string text;
        try
        {
            // Считываем текст из файла
            text = File.ReadAllText(filePath);
        }
        catch
        {
            Console.WriteLine("Ошибка при чтении файла");
            return;
        }

        // Разделяем текст на слова (используем Split и удаляем лишние символы)
        string[] words = text.Split(new char[] 
        { ' ', '\t', '\n', '\r', '.', ',', '!', '?', ':', ';', '-', '—', '(', ')' }, 
        StringSplitOptions.RemoveEmptyEntries);

        // Хэш-таблицы для учета символов
        var singleOccurrence = new HashSet<char>();
        var multipleOccurrence = new HashSet<char>();

        // Проходим по каждому слову из текста
        foreach (var word in words)
        {
            // Временный HashSet для уникальных символов текущего слова
            HashSet<char> wordChars = new HashSet<char>();

            // Обрабатываем каждый символ в слове
            foreach (char c in word)
            {
                // Проверяем, является ли символ буквой (игнорируем цифры и символы)
                if (char.IsLetter(c) && !wordChars.Contains(c))
                {
                    // Если символ уже был в singleOccurrence, переносим его в multipleOccurrence
                    if (singleOccurrence.Contains(c))
                    {
                        singleOccurrence.Remove(c); // Удаляем из singleOccurrence
                        multipleOccurrence.Add(c); // Добавляем в multipleOccurrence
                    }
                    // Если символ отсутствует и в singleOccurrence, и в multipleOccurrence
                    else if (!multipleOccurrence.Contains(c))
                    {
                        singleOccurrence.Add(c); // Добавляем символ в singleOccurrence
                    }

                    // Добавляем символ в wordChars, чтобы избежать его повторной обработки в этом слове
                    wordChars.Add(c);
                }
            }
        }

        // Оставшиеся символы в singleOccurrence - те, что встречаются только в одном слове
        Console.WriteLine("Символы, которые встречаются в одном и только в одном слове:");
        foreach (var c in singleOccurrence)
        {
            Console.Write(c + " ");
        }
    }


    // Задание 5
    public static void Exercise5()
    {
        Console.WriteLine("Задание 5.");

        // Задаём имя файла
        string fileName = "ex5.xml";

        // Проверяем его существование
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        // Получаем количество студентов для занесения в файл
        Console.Write("Введите количество студентов для занесения в файл: ");
        uint n;  // Количество студентов
        try
        {
            n = Convert.ToUInt32(Console.ReadLine());
            if (n == 0) throw new Exception();
        }
        catch
        {
            Console.WriteLine("Неправильно введены данные");
            return;
        }
        Console.WriteLine();

        // Заполнение исходного файла
        MakeFileXml(fileName, n);

        // Чтение исходного файла
        List<Student> students = ReadFileXml(fileName);

        // Поиск минимального балла среди отличников
        int thresholdScore = FindThreshold(students);
        Console.WriteLine(thresholdScore);
    }

    // Заполнение XML-файла (сериализация)
    private static void MakeFileXml(string filePath, uint n)
    {
        // Создаём списки имен и фамилий студентов для генерации
        List<string> names = new List<string> { 
            "Иван", "Фёдор", "Михаил", "Виктор", 
            "Александр", "Григорий", "Дмитрий", 
            "Анатолий", "Виталий", "Егор", "Илья" 
        };

        List<string> firstNames = new List<string> {
            "Сидоров", "Афанасьев", "Петров", "Борцов",
            "Мухин", "Носов", "Миронов", "Курицин",
            "Кошкин", "Волев", "Миров", "Куприн"
        };

        // Генерируем список студентов
        Random random = new Random();
        List<Student> students = new List<Student>();

        for (int i = 0; i < n; i++)
        {
            try
            {
                students.Add(new Student
                    (firstNames[random.Next(firstNames.Count)], 
                    names[random.Next(names.Count)], 
                    random.Next(1, 100), random.Next(1, 101)));
            } catch
            {
                Console.WriteLine("Неправильный ввод данных");
                return;
            }
        }

        // Вывод для наглядности
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(students[i].Surname + " " + students[i].Name + " " + students[i].SchoolNumber + " " + students[i].Score);
        }

        // Используется сериализация в файл
        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, students);
        }
    }

    // Чтение XML-файла (сериализация)
    private static List<Student> ReadFileXml(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                return (List<Student>)serializer.Deserialize(reader);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при чтении XML: " + ex.Message);
            return new List<Student>();
        }
    }

    // Нахождение минимального балла среди отличников
    private static int FindThreshold(List<Student> students)
    {
        // Создаем словарь для хранения количества студентов с каждым уникальным количеством баллов.
        Dictionary<int, int> scoreCount = new Dictionary<int, int>();

        // Проходим по каждому студенту и подсчитываем количество студентов с одинаковыми баллами.
        foreach (var student in students)
        {
            if (scoreCount.ContainsKey(student.Score))
                scoreCount[student.Score]++;
            else
                scoreCount[student.Score] = 1;
        }

        // Определяем целевое количество студентов (20% от общего числа студентов).
        int targetCount = students.Count / 5;

        // Получаем список уникальных баллов, отсортированных в порядке убывания.
        var sortedScores = scoreCount.Keys.OrderByDescending(x => x).ToList();

        // Инициализируем переменную для подсчета студентов.
        int count = 0;

        // Переменная для хранения минимального балла среди топ-20%
        int thresholdScore = 0;

        // Проходим по отсортированным баллам.
        foreach (var score in sortedScores)
        {
            // Увеличиваем общее количество студентов с текущим баллом.
            count += scoreCount[score];

            // Когда количество студентов достигает или превышает целевого числа, устанавливаем порог.
            if (count >= targetCount)
            {
                thresholdScore = score;
                break;
            }
        }

        // Возвращаем минимальный балл среди высоких (топ-20%).
        return thresholdScore;
    }
}
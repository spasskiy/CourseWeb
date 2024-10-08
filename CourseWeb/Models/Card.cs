﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWeb.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public Card? Next { get; set; }
        public int Priority { get; set; }

        // Статический метод-фабрика для создания объекта Card
        public static void AddCard(Card head, string front, string back, int priority)
        {
            Card tmp = head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Next = new Card
            {
                Front = front,
                Back = back,
                Priority = priority
            };
        }

        public static void InsertCardAtSecondPosition(Card head, string front, string back, int priority)
        {
            if (head.Next == null)
            {
                // Если в колоде только одна карточка, просто добавляем новую карточку в конец
                AddCard(head, front, back, priority);
            }
            else
            {
                // Создаем новую карточку
                var newCard = new Card
                {
                    Front = front,
                    Back = back,
                    Priority = priority,
                    Next = head.Next
                };

                // Вставляем новую карточку на вторую позицию
                head.Next = newCard;
            }
        }

        public Card MoveNext(Card head, bool notRemember = false)
        {
            Console.WriteLine();
            Console.WriteLine(head.Priority);
            if (!notRemember)
                head.Priority = head.Priority + 1;
            Console.WriteLine(head.Priority);

            if (head.Next is null)
                return head;
            Card result = head.Next;
            Card runner = head;

            do
            {
                Console.WriteLine($"runner = {runner.Front} and priority is {runner.Priority}");
                if (runner.Next != null)
                {
                    Console.WriteLine($"runner.Next = {runner.Next.Front} and priority is {runner.Next.Priority}");
                }
                runner = runner.Next;
                Console.WriteLine("**************************************************************************");
                Console.WriteLine($"runner.Next != null && runner.Next.Priority <= head.Priority is {runner.Next != null && runner.Next.Priority <= head.Priority}");
                Console.WriteLine($"runner.Next != null is {runner.Next != null}");
                Console.WriteLine($"runner.Next.Priority <= head.Priority is {runner.Next != null && runner.Next.Priority <= head.Priority}");
                Console.WriteLine($"runner.Next.Priority <= head.Priority is {runner.Next?.Priority} <= {head.Priority}");
            }
            while (runner.Next != null && runner.Next.Priority <= head.Priority);

            head.Next = runner.Next;
            runner.Next = head;

            Console.WriteLine($"Возвращаем {result.Front}");
            return result;
        }
    }

}
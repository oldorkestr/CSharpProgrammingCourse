using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpProgrammingCourse.Lecture7;

    // --- 1. КЛАСИ ВИНЯТКІВ КОРИСТУВАЧА ---
    public class InsufficientLiquidityException : Exception
    {
        public string Ticker { get; }
        public InsufficientLiquidityException(string message, string ticker) : base(message) 
            => Ticker = ticker;
    }

    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(string message) : base(message) { }
    }

    // Модель транзакції
    public record Transaction(string Ticker, decimal Amount, bool IsBuy);

    public class CryptoManager
    {
        // --- 2. КОЛЕКЦІЇ .NET ---
        
        // Словник для швидкого доступу до балансів (Ticker -> Balance)
        private Dictionary<string, decimal> _balances = new();

        // Сортований список для ринкових цін (автоматичне сортування за назвою)
        private SortedList<string, decimal> _marketPrices = new();

        // Черга для обробки вхідних заявок (Pending Orders)
        private Queue<Transaction> _pendingOrders = new();

        // Стек для скасування останніх успішних операцій (Undo)
        private Stack<Transaction> _history = new();

        // Бітовий масив для налаштувань портфеля (напр. [0]-Сповіщення, [1]-Автостейкінг, [2]-Прихований)
        private BitArray _portfolioSettings = new BitArray(4);

        public CryptoManager()
        {
            // Ініціалізація дефолтних цін
            _marketPrices.Add("BTC", 65000m);
            _marketPrices.Add("ETH", 3500m);
            _marketPrices.Add("SOL", 145m);
            
            // Налаштування: Увімкнути сповіщення [0] та Автостейкінг [1]
            _portfolioSettings.Set(0, true);
            _portfolioSettings.Set(1, true);
        }

        public void AddOrderToQueue(string ticker, decimal amount, bool isBuy)
        {
            _pendingOrders.Enqueue(new Transaction(ticker, amount, isBuy));
        }

        // --- 3. ОПРАЦЮВАННЯ ТА ЗАПУСК ВИНЯТКІВ ---
        public void ProcessOrders()
        {
            Console.WriteLine($"--- Обробка черги замовлень ({_pendingOrders.Count} шт.) ---");

            while (_pendingOrders.Count > 0)
            {
                Transaction order = _pendingOrders.Dequeue();
                try
                {
                    ExecuteTransaction(order);
                }
                catch (InsufficientLiquidityException ex)
                {
                    Console.WriteLine($"[ПОМИЛКА ЛІКВІДНОСТІ]: {ex.Message}. Тікер: {ex.Ticker}");
                }
                catch (AssetNotFoundException ex)
                {
                    Console.WriteLine($"[КРИТИЧНА ПОМИЛКА]: {ex.Message}");
                    // Re-throw або логування
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[НЕПЕРЕДБАЧЕНА ПОМИЛКА]: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Завершено спробу обробки {order.Ticker}.");
                }
            }
        }

        private void ExecuteTransaction(Transaction order)
        {
            // Перевірка наявності активу в маркеті
            if (!_marketPrices.ContainsKey(order.Ticker))
                throw new AssetNotFoundException($"Актив {order.Ticker} не торгується на біржі.");

            if (order.IsBuy)
            {
                if (!_balances.ContainsKey(order.Ticker)) _balances[order.Ticker] = 0;
                _balances[order.Ticker] += order.Amount;
                _history.Push(order); // Додаємо в стек для Undo
                Console.WriteLine($"Куплено {order.Amount} {order.Ticker}");
            }
            else
            {
                // Перевірка ліквідності (чи вистачає монет на продаж)
                if (!_balances.ContainsKey(order.Ticker) || _balances[order.Ticker] < order.Amount)
                {
                    throw new InsufficientLiquidityException("Недостатньо коштів для продажу", order.Ticker);
                }

                _balances[order.Ticker] -= order.Amount;
                _history.Push(order);
                Console.WriteLine($"Продано {order.Amount} {order.Ticker}");
            }
        }

        public void UndoLastTransaction()
        {
            if (_history.Count == 0) return;

            var last = _history.Pop();
            Console.WriteLine($"--- СКАСУВАННЯ ОПЕРАЦІЇ: {last.Ticker} ---");
            
            // Логіка реверсу (якщо купували - віднімаємо, і навпаки)
            if (last.IsBuy) _balances[last.Ticker] -= last.Amount;
            else _balances[last.Ticker] += last.Amount;
        }

        public void ShowPortfolio()
        {
            Console.WriteLine("\n--- ПОТОЧНИЙ ПОРТФЕЛЬ ---");
            foreach (var item in _balances)
            {
                decimal price = _marketPrices[item.Key];
                Console.WriteLine($"{item.Key}: {item.Value} (Ціна: ${price} | Разом: ${item.Value * price})");
            }
            
            if (_portfolioSettings[0]) Console.WriteLine("(!) Сповіщення увімкнені.");
        }
    }

    public static class Lecture7
    {
        public static void Demo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CryptoManager manager = new CryptoManager();

            // Додаємо замовлення в чергу
            manager.AddOrderToQueue("BTC", 0.5m, true);  // Ок
            manager.AddOrderToQueue("ETH", 2.0m, true);  // Ок
            manager.AddOrderToQueue("DOGE", 100m, true); // Викличе AssetNotFoundException
            manager.AddOrderToQueue("BTC", 1.0m, false); // Викличе InsufficientLiquidityException

            // Запуск обробки
            manager.ProcessOrders();

            manager.ShowPortfolio();

            // Скасування останньої дії
            manager.UndoLastTransaction();
            manager.ShowPortfolio();
        }
    }
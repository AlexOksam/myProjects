using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Timers;

namespace Number_Guessing_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string gamesPlayed, bestGame;
            int secretNumber, guessNumber = 0, counter = 0;

            secretNumber = new Random().Next(0, 1000);

            string connectionString = "Data Source = DESKTOP********\\SQLEXPRESS; Initial Catalog=number_guess; Integrated Security=true";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            Console.WriteLine("Number Guessing Game");
            Console.WriteLine(String.Empty);
            Console.WriteLine("Enter your username:");       
            
            string username = Console.ReadLine();

            string availableUser = $"SELECT username FROM users WHERE username = '{username}';";
            SqlCommand cmd = new SqlCommand(availableUser, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows == false) // (reader.GetValue(0).ToString() != username)
            {
                conn.Close();
                conn.Open();
                string insertUser = $"INSERT INTO users (username) VALUES ('{username}');";
                SqlCommand insertCmd = new SqlCommand(insertUser, conn);
                insertCmd.ExecuteNonQuery();
                conn.Close();

                Console.WriteLine($"Welcome,´{username}! It looks like this is your first time here.");
            }
            else 
            {
                conn.Close();
                conn.Open();
                string gamesPlayedQuery = $"SELECT COUNT(*) FROM games AS g INNER JOIN users AS u on g.user_id = u.user_id WHERE u.username = '{username}';";
                SqlCommand gamesPlayedCmd = new SqlCommand(gamesPlayedQuery, conn);
                SqlDataReader gamesPlayedReader = gamesPlayedCmd.ExecuteReader();
                gamesPlayedReader.Read();
                gamesPlayed = gamesPlayedReader.GetValue(0).ToString();
                conn.Close();

                conn.Open();
                string bestGameQuery = $"SELECT MIN(count_guesses) FROM games AS g INNER JOIN users AS u on g.user_id = u.user_id WHERE u.username = '{username}';";
                SqlCommand bestGameCmd = new SqlCommand(bestGameQuery, conn);
                SqlDataReader bestGameReader = bestGameCmd.ExecuteReader();
                bestGameReader.Read();
                bestGame = bestGameReader.GetValue(0).ToString();
                conn.Close();

                Console.WriteLine(String.Empty);
                Console.WriteLine($"Welcome back, {username}! You have played {gamesPlayed} games, and your best game took {bestGame} guesses.");
            }

            conn.Open();
            string getUserId = $"SELECT user_id FROM users WHERE username = '{username}';";
            SqlCommand getUserCmd = new SqlCommand(getUserId, conn);
            SqlDataReader getUserReader = getUserCmd.ExecuteReader();
            getUserReader.Read();
            
            int userIdInt = Int32.Parse(getUserReader.GetValue(0).ToString());
            conn.Close();

            Console.WriteLine(String.Empty);
            Console.Write("Guess a number between 1 and 100: ");

            while (guessNumber != secretNumber)
            {
                
                if (!int.TryParse(Console.ReadLine(), out guessNumber))
                {
                    Console.Write("That is not an integer, guess again: ");
                }
                else if (guessNumber > secretNumber)
                {
                    Console.Write("It's lower than that, guess again: ");
                }
                else if (guessNumber < secretNumber)
                {
                    Console.Write("It's higher than that, guess again: ");
                }
                counter++;
            }
            Console.WriteLine(String.Empty);
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine(String.Empty);

            conn.Open();
            string inserGame = $"INSERT INTO games(count_guesses, user_id) VALUES ({counter}, {userIdInt});";
            SqlCommand insertGameCmd = new SqlCommand(inserGame, conn);
            insertGameCmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine($"You guessed it in {counter} tries. The secret number was {secretNumber}. Nice job!");
            Console.WriteLine(String.Empty);

        }
    }
}

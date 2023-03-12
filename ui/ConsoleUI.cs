using System;
using System.Collections.Generic;
using MAP_lab8.service;

namespace MAP_lab8.ui
{
    public class ConsoleUI
    {
        private Service service;

        public ConsoleUI()
        {
            service = new Service("players.txt", "teams.txt", "games.txt", "activeplayers.txt");
            Run();
        }
        
        void Run()
        {
            int option=1;
            while (option!=0)
            {
                Console.WriteLine("1.Show players of team");
                Console.WriteLine("2.Show active players of team on game");
                Console.WriteLine("3.Show games in time period");
                Console.WriteLine("4.Show the score of a game");
                option=Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        GetPlayersOfTeam();
                        break;
                    case 2:
                        GetActivePlayersOfTeamGame();
                        break;
                    case 3:
                        GetGamesInPeriod();
                        break;
                    case 4:
                        GetScoreOfGame();
                        break;
                }
                
                Console.WriteLine();
            }
        }

        void GetPlayersOfTeam()
        {
            Console.WriteLine("Id of team:");
            long teamId = Convert.ToInt64(Console.ReadLine());
            List<Player<long>> players = service.GetPlayersOfTeam(teamId);
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
        }

        void GetActivePlayersOfTeamGame()
        {
            Console.WriteLine("Id of team:");
            long teamId = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Id of game:");
            long gameId = Convert.ToInt64(Console.ReadLine());
            List<Player<long>> players = service.GetActivePlayersOfTeamGame(teamId, gameId);
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }   
        }

        void GetGamesInPeriod()
        {
            Console.WriteLine("Start date:");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("End date:");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());
            List<Game<long>> games = service.GetGamesInPeriod(startDate, endDate);
            foreach (var game in games)
            {
                Console.WriteLine(game);
            }   
        }

        void GetScoreOfGame()
        {
            Console.WriteLine("Id of game:");
            long gameId = Convert.ToInt64(Console.ReadLine());
            var scoresOfTeams = service.GetScoreOfGame(gameId);
            foreach (var scoresOfTeam in scoresOfTeams)
            {
                Console.WriteLine("Team " + scoresOfTeam.Key + ": " + scoresOfTeam.Value + " points");
            }
        }
    }
}
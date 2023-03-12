using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using MAP_lab8.repository;

namespace MAP_lab8.service
{
    public class Service
    {
        private FileRepository<long, Team<long>> teamRepository;
        private FileRepository<long, Player<long>> playerRepository;
        private FileRepository<long, Game<long>> gameRepository;
        private FileRepository<long, ActivePlayer<long>> activePlayerRepository;

        public Service(string playersFile,  string teamsFile, string gamesFile, string activePlayersFile)
        {
            playerRepository = new FileRepository<long, Player<long>>(playersFile, CreatePlayerFromLine);
            teamRepository = new FileRepository<long, Team<long>>(teamsFile, CreateTeamFromLine);
            gameRepository = new FileRepository<long, Game<long>>(gamesFile, CreateGameFromLine);
            activePlayerRepository = new FileRepository<long, ActivePlayer<long>>(activePlayersFile, CreateActivePlayerFromLine);
        }

        Student<long> CreateStudentFromLine(string line)
        {
            string[] vals= line.Split(',');
            long id = Convert.ToInt64(vals[0]);
            string name = vals[1];
            string school = vals[2];
            return new Student<long>(id, name, school);
        }
        
        Player<long> CreatePlayerFromLine(string line)
        {
            string[] vals= line.Split(',');
            long id = Convert.ToInt64(vals[0]);
            string name = vals[1];
            string school = vals[2];
            string teamString = vals[3].Substring(1, vals[3].Length-1) + "," + vals[4].Substring(0, vals[4].Length - 1);
            Team<long> team = CreateTeamFromLine(teamString);
            return new Player<long>(id, name, school, team);
        }
        
        Team<long> CreateTeamFromLine(string line)
        {
            string[] vals= line.Split(',');
            long id = Convert.ToInt64(vals[0]);
            string name = vals[1];
            return new Team<long>(id, name);
        }
        
        Game<long> CreateGameFromLine(string line)
        {
            string[] vals= line.Split(',');
            long id = Convert.ToInt64(vals[0]);
            string team1String = vals[1].Substring(1, vals[1].Length-1) + "," + vals[2].Substring(0, vals[2].Length - 1);
            Team<long> team1 = CreateTeamFromLine(team1String); string team2String = vals[3].Substring(1, vals[3].Length-1) + "," + vals[4].Substring(0, vals[4].Length - 1);
            Team<long>team2 = CreateTeamFromLine(team2String);            
            DateTime date = DateTime.Parse(vals[5]);
            return new Game<long>(id, team1, team2, date);
        }
        
        ActivePlayer<long> CreateActivePlayerFromLine(string line)
        {
            string[] vals= line.Split(',');
            long id = Convert.ToInt64(vals[0]);
            long idPlayer = Convert.ToInt64(vals[1]);
            long idGame = Convert.ToInt64(vals[2]);
            int nbPointsScored = Convert.ToInt32(vals[3]);
            PlayerType playerType = vals[4] == "Playing" ? PlayerType.Playing : PlayerType.Extra;
            return new ActivePlayer<long>(id, idPlayer, idGame, nbPointsScored, playerType);
        }

        public List<Player<long>> GetPlayersOfTeam(long teamId)
        {
            return playerRepository.FindAll().Where(p => p.Team.Id==teamId).ToList();
        }

        public List<Player<long>> GetActivePlayersOfTeamGame(long teamId, long gameId)
        {
            List<Player<long>> players=new List<Player<long>>();
            foreach (var activePlayer in activePlayerRepository.FindAll())
            {
                var player = playerRepository.FindOne(activePlayer.IdPlayer);
                if (activePlayer.IdGame == gameId && player.Team.Id==teamId)
                {
                    players.Add(player);
                }
            }

            return players;
        }

        public List<Game<long>> GetGamesInPeriod(DateTime startDate, DateTime endDate)
        {
            return gameRepository.FindAll().Where(game => game.Date >= startDate && game.Date <= endDate).ToList();
        }

        public Dictionary<long, int> GetScoreOfGame(long gameId)
        {
            Dictionary<long, int> scoresOfTeams = new Dictionary<long, int>();//<teamId, score>

            foreach (var activePlayer in activePlayerRepository.FindAll())
            {
                if (activePlayer.IdGame == gameId)
                {
                    var player = playerRepository.FindOne(activePlayer.IdPlayer);
                    if(scoresOfTeams.ContainsKey(player.Team.Id))
                        scoresOfTeams[player.Team.Id] += activePlayer.NbPointsScored;
                    else 
                        scoresOfTeams.Add(player.Team.Id, activePlayer.NbPointsScored);   
                }
            }
            return scoresOfTeams;
        }
    }
}
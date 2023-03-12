using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace MAP_lab8.repository
{
    public class FileRepository<ID, E>  : IRepository<ID, E> where E : Entity<ID>
    {
        private Dictionary<ID, E> entities;
        private String fileName;
        private CreateEntityFromLine createEntityFromLineDelegate;

        public FileRepository(string fileName, CreateEntityFromLine createEntityFromLine)
        {
            this.fileName = fileName;
            createEntityFromLineDelegate = createEntityFromLine;
            entities = new Dictionary<ID, E>();
            LoadFromFile();
        }

        public CreateEntityFromLine CreateEntityFromLineDelegate { get; set; }
        
        /**
         * Creates Entity from file line - needs to be implemented and given to FileRepository constructor
         */
        public delegate E CreateEntityFromLine(string line);

        public E FindOne(ID id)
        {
            if (id == null)
            {
                throw new ArgumentException();
            }
            
            if (entities.ContainsKey(id))
            {
                return entities[id];
            }

            return null;
        }

        public IEnumerable<E> FindAll()
        {
            return entities.Values;
        }

        public E Save(E entity)
        {
            if (entities.ContainsKey(entity.Id))
            {
                return entity;
            }
            entities.Add(entity.Id, entity);
            SaveToFile();
            return null;
        }

        public E Delete(ID id)
        {
            E entity = FindOne(id);
            if (entity == null) return null;
            entities.Remove(id);
            SaveToFile();
            return entity;

        }

        public E Update(E entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            if (!entities.ContainsKey(entity.Id)) return entity;
            entities[entity.Id] = entity;
            SaveToFile();
            return null;

        }

        

        /**
         * Load data from file into memory using CreateEntityFromFile
         */
        private void LoadFromFile()
        {
            if (!File.Exists(fileName)) { using (StreamWriter sw = File.CreateText(fileName)) {sw.Close();} }

            try{
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        E entity = createEntityFromLineDelegate(line);
                        entities.Add(entity.Id, entity);
                    }

                    sr.Close();
                }
            }catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        /**
         * Save data to file
         */
        private void SaveToFile()
        {
            try{
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    foreach (E entity in entities.Values)
                    {
                        sw.WriteLine(entity);
                    }

                    sw.Close();
                }
            }catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
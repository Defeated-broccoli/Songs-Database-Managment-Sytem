using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using WPF_SQL_CRUD.Models;
using WPF_SQL_CRUD.ViewModels;

namespace WPF_SQL_CRUD
{
    public static class DataAccess
    {
        public static ObservableCollection<SongViewModel> GetTable()
        {
            string cs = @"Server=localhost\SQLEXPRESS;Database=MUSIC;Trusted_Connection=True;";

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string query = "Select * From Music.dbo.Songs order by Title";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            ObservableCollection<SongViewModel> list = new ObservableCollection<SongViewModel>();

            while (reader.Read())
            {
                list.Add(new SongViewModel()
                {
                    Title = reader.GetString(0).Replace("*", "'"),
                    Author = reader.GetString(1).Replace("*", "'"),
                    ReleaseDate = DateOnly.FromDateTime(reader.GetDateTime(2))
                });
            }

            Trace.WriteLine(query);
            con.Close();

            return list;
        }

        public static void AddToTable(SongViewModel song)
        {
            string cs = @"Server=localhost\SQLEXPRESS;Database=MUSIC;Trusted_Connection=True;";

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string title = string.Empty;
            string author = string.Empty;

            if (song.Title != null)
            {
                title = song.Title.Replace("'", "*");
            }
            
            if(song.Author != null)
            {
                author = song.Author.Replace("'", "*");
            }

            string query = $"insert into Music.dbo.Songs (Title, Author, ReleaseDate) values ('{title}', '{author}', '{song.ReleaseDate.ToString()}')";
            Trace.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            
            con.Close();
        }


        public static void DeleteFromTable(List<SongViewModel> song)
        {
            string cs = @"Server=localhost\SQLEXPRESS;Database=MUSIC;Trusted_Connection=True;";

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string query = string.Empty;

            string title = string.Empty;
            string author = string.Empty;

            for(int i = 0; i < song.Count; i++)
            {
                title = song[i].Title.Replace("'", "*");
                author = song[i].Author.Replace("'", "*");
                string tempQuery = $"DELETE FROM Music.dbo.Songs WHERE title ='{title}' and Author = '{author}';";
                query = query + tempQuery ;
            }

            
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            Trace.WriteLine(query);
            con.Close();
        }

        public static void EditInTable(SongViewModel deleteSong, SongViewModel newSong)
        {
            string cs = @"Server=localhost\SQLEXPRESS;Database=MUSIC;Trusted_Connection=True;";

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string title = string.Empty;
            string author = string.Empty;

            if (newSong.Title != null)
            {
                title = newSong.Title.Replace("'", "*");
            }

            if (newSong.Author != null)
            {
                author = newSong.Author.Replace("'", "*");
            }

            string query = $"Update Music.dbo.Songs set title='{title}', Author = '{author}', ReleaseDate = '{newSong.ReleaseDate.ToString()}' WHERE title ='{deleteSong.Title}' and Author = '{deleteSong.Author}' and ReleaseDate = '{deleteSong.ReleaseDate.ToString()}'";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            Trace.WriteLine(query);
            con.Close();
        }

    }
}

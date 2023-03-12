using System;
using System.Windows.Media;
using WPF_SQL_CRUD.Models;
using WPF_SQL_CRUD.ViewModels;

namespace WPF_SQL_CRUD.ViewModels
{
    public class SongViewModel : ObservableObject
    {
        Song Song { get; set; } = new Song();
        public string Title
        {
            get
            {
                return Song.Title;
            }
            set
            {
                Song.Title = value;
            }
        }
        public string Author
        {
            get
            {
                return Song.Author;
            }
            set
            {
                Song.Author = value;
            }
        }
        
        public DateOnly ReleaseDate
        {
            get
            {
                return Song.ReleaseDate;
            }
            set
            {
                Song.ReleaseDate = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return Song.IsSelected;
            }
            set
            {
                Song.IsSelected = value;
                OnPropertyChanged(nameof(Song.IsSelected));
            }
        }
    }
}

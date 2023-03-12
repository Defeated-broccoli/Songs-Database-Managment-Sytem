using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WPF_SQL_CRUD.Views;

namespace WPF_SQL_CRUD.ViewModels
{
    public class SongListViewModel : ObservableObject
    {
        public static ObservableCollection<SongViewModel> Songs { get; set; } = new ObservableCollection<SongViewModel>();
        private string _NewTitle;
        public string NewTitle 
        { 
            get
            {
                return _NewTitle;
            }
            set
            {
                _NewTitle = value;
                OnPropertyChanged(nameof(NewTitle));
            }
        }
        private string _NewAuthor;
        public string NewAuthor 
        { 
            get
            {
                return _NewAuthor;
            }
            set
            {
                _NewAuthor = value;
                OnPropertyChanged(nameof(NewAuthor));
            }
        }
        private DateOnly _NewReleaseDate;
        public DateOnly NewReleaseDate 
        { 
            get
            {
                return _NewReleaseDate;
            }
            set
            {
                _NewReleaseDate = value;
                OnPropertyChanged(nameof(NewReleaseDate));
            }
        }

        private bool _CheckBox = false;
        public bool CheckBox 
        {
            get
            {
                return _CheckBox;
            }
            set
            {
                _CheckBox = value;
                Check(value);
                OnPropertyChanged(nameof(CheckBox));
            }
        }

        public ICommand OpenNewSongWindowCommand { get; set; }
        public ICommand AddNewSongCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand EditSongCommand { get; set; }        

        public SongListViewModel()
        {
            Songs = DataAccess.GetTable();
            NewReleaseDate = DateOnly.FromDateTime(DateTime.Now);

            AddNewSongCommand = new RelayCommand(AddNewSong);
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDelete);
            EditSongCommand = new RelayCommand(EditSong, CanEdit);
        }

        public void AddNewSong(object obj)
        {
            bool orginal = true;
            for (int i = Songs.Count - 1; i >= 0; i--)
            {
                if ((Songs[i].Title == NewTitle) && (Songs[i].Author == NewAuthor))
                {
                    orginal = false; break;
                }
            }

            if (orginal == true)
            {
                SongViewModel song = new SongViewModel()
                {
                    Title = NewTitle,
                    Author = NewAuthor,
                    ReleaseDate = NewReleaseDate
                };
                Songs.Add(song);
                DataAccess.AddToTable(song);

                NewTitle = string.Empty;
                NewAuthor = string.Empty;
                NewReleaseDate = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                MessageBox.Show("This song is already in the database!");
            }
        }

        //public bool yes(object obj)
        //{ return true; }


        public void DeleteSong(object obj)
        {
            List<SongViewModel> temp = new List<SongViewModel>();

            for (int i = Songs.Count - 1; i >= 0; i--)
            {
                if (Songs[i].IsSelected == true)
                {
                    temp.Add(Songs[i]);
                    Songs.RemoveAt(i);
                }
            }

            DataAccess.DeleteFromTable(temp);

            CheckBox = false;
        }

        public bool CanDelete(object obj)
        {
            
            for (int i = Songs.Count - 1; i >= 0; i--)
            {
                if(Songs[i].IsSelected == true)
                {
                    return true; 
                }
            }
            return false;
        }

        public void EditSong(object obj)
        {
            bool orginal = true;
            for (int i = Songs.Count - 1; i >= 0; i--)
            {
                if ((Songs[i].Title == NewTitle) && (Songs[i].Author == NewAuthor))
                {
                    orginal = false; break;
                }
            }
            if (orginal == true)
            {            
                for (int i = Songs.Count - 1; i >= 0; i--)
                {
                    if (Songs[i].IsSelected == true)
                    {
                        SongViewModel oldSong = Songs[i];
                        Songs[i] = new SongViewModel()
                        {
                            Title = NewTitle,
                            Author = NewAuthor,
                            ReleaseDate = NewReleaseDate
                        };

                        DataAccess.EditInTable(oldSong, Songs[i]);
                    }
                }

                
                NewTitle = string.Empty;
                NewAuthor = string.Empty;
                NewReleaseDate = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                MessageBox.Show("This song is already in the database!");
            }

        }

        public bool CanEdit(object obj)
        {
            int n = 0;
            for (int i = Songs.Count - 1; i >= 0; i--)
            {
                if (Songs[i].IsSelected == true)
                {
                    n++;
                }
            }

            if (n == 1)
            {
                return true;
            }
            else 
            {
                return false; 
            }

        }

        public void Check(bool value)
        {

            if (value == true)
            {
                for (int i = Songs.Count - 1; i >= 0; i--)
                {
                    Songs[i].IsSelected = true;
                }
            }
            else
            {
                for (int i = Songs.Count - 1; i >= 0; i--)
                {
                    Songs[i].IsSelected = false;
                }
            }
        }
    }


}

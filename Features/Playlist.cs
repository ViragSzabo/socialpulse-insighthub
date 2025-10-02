using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociallyAnxiousHub.Features
{
    class Playlist
    {
        private List<Song> _songs;

        public Playlist()
        {
            _songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            if (song != null && !_songs.Contains(song))
            {
                _songs.Add(song);
            }
        }

        public void RemoveSong(Song song)
        {
            if (song != null && _songs.Contains(song))
            {
                _songs.Remove(song);
            }
        }

        public List<Song> GetSongs()
        {
            return new List<Song>(_songs);
        }

        public void ClearPlaylist()
        {
            _songs.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Playlist:");
            foreach (var song in _songs)
            {
                sb.AppendLine(song.ToString());
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Playlist playlist)
            {
                return _songs.SequenceEqual(playlist._songs);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _songs.Aggregate(0, (hash, song) => hash ^ song.GetHashCode());
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = _songs.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                Song temp = _songs[n];
                _songs[n] = _songs[k];
                _songs[k] = temp;
            }
        }

        public void SortByTitle()
        {
            _songs.Sort((x, y) => string.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase));
        }

        public void SortByArtist()
        {
            _songs.Sort((x, y) => string.Compare(x.Artist, y.Artist, StringComparison.OrdinalIgnoreCase));
        }

        public void SortByAlbum()
        {
            _songs.Sort((x, y) => string.Compare(x.Album, y.Album, StringComparison.OrdinalIgnoreCase));
        }
    }
}
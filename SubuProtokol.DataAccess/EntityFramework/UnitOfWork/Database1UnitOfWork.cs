using SubuProtokol.DataAccess.Base.UnitOfWork;
using SubuProtokol.DataAccess.EntityFramework.Context;
using SubuProtokol.DataAccess.EntityFramework.Repositories;

namespace SubuProtokol.DataAccess.EntityFramework.UnitOfWork
{

    public interface IDatabase1UnitOfWork : IUnitOfWork
    {
        //IAlbumRepository AlbumRepository { get; }
        //ISongRepository SongRepository { get; }
        //IArtistRepository ArtistRepository { get; }
    }

    public class Database1UnitOfWork : IDatabase1UnitOfWork
    {
        private readonly Databse1Context _context;

        public Database1UnitOfWork(Databse1Context context)
        {
            _context = context;
        }


        //private IAlbumRepository albumRepository;

        //public IAlbumRepository AlbumRepository
        //{
        //    get
        //    {
        //        if (albumRepository == null)
        //        {
        //            albumRepository = new AlbumRepository(_context);
        //        }

        //        return albumRepository;
        //    }
        //}

        //private ISongRepository songRepository;

        //public ISongRepository SongRepository
        //{
        //    get
        //    {
        //        if (songRepository == null)
        //        {
        //            songRepository = new SongRepository(_context);
        //        }

        //        return songRepository;
        //    }
        //}

        //private IArtistRepository artistRepository;

        //public IArtistRepository ArtistRepository
        //{
        //    get
        //    {
        //        if (artistRepository == null)
        //        {
        //            artistRepository = new ArtistRepository(_context);
        //        }

        //        return artistRepository;
        //    }
        //}

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}

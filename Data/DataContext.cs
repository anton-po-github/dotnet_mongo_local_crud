﻿﻿using MongoDB.Driver;
using MongoDB.Driver.GridFS;
public class DataContext
{
    private readonly IMongoDatabase _database = null;
    private readonly GridFSBucket _bucket = null;
    private readonly IDatabaseSettings _databaseSettings = null;

    public DataContext(IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        _databaseSettings = databaseSettings;
        _database = client.GetDatabase(databaseSettings.DatabaseName);

        var gridFsBucketOptions = new GridFSBucketOptions()
        {
            BucketName = "images",
            ChunkSizeBytes = 1048576, // 1МБ
        };

        _bucket = new GridFSBucket(_database, gridFsBucketOptions);
    }

    public IMongoCollection<Book> Books
    {
        get
        {
            return _database.GetCollection<Book>(_databaseSettings.BooksCollectionName);
        }
    }

    public GridFSBucket Bucket
    {
        get
        {
            return _bucket;
        }
    }
    public IMongoDatabase MongoDatabase
    {
        get
        {
            return _database;
        }
    }

}
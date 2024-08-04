using SourceGeneratorSample.Data;

var dbContext = new AppDbContext();

// dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

// 


// CONSTRAINT FK_Left FOREIGN KEY(LId) REFERENCES Table1(Id)
// CONSTRAINT FK_Right FOREIGN KEY(RId) REFERENCES Table2(Id)
// CONSTRAINT PK_Blabla PRIMARY KEY(LId, RId)
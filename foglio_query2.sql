use soluzione_bankomat

select * from Banche
select * from Utenti
select * from Admin
drop table Admin
CREATE TABLE Admin (
    IdAdmin INT IDENTITY(1,1) PRIMARY KEY not null,
    IdBanca BIGINT,
	IsAdmin bit not null, 
    Username VARCHAR(25) not null,
    Password VARCHAR(50) not null,
    FOREIGN KEY (IdBanca) REFERENCES Banche(Id)
);

INSERT INTO Admin (IdBanca, IsAdmin, Username, Password) VALUES (1, 1, 'admin', 'admin');
INSERT INTO Admin (IdBanca, IsAdmin, Username, Password) VALUES (2, 1, 'luca', 'acul');
INSERT INTO Admin (IdBanca, IsAdmin, Username, Password) VALUES (3, 1, 'marcoAdmin', '12345');
INSERT INTO Admin (IdBanca, IsAdmin, Username, Password) VALUES (4, 1, 'amministratore', 'amministratore');
INSERT INTO Admin (IdBanca, IsAdmin, Username, Password) VALUES (5, 1, 'giacomo', 'giacomo1144');

ALTER TABLE utenti
ADD isAdmin bit;

UPDATE utenti
SET isAdmin = 0
WHERE Id = 18;

ALTER TABLE nome_tabella
DROP COLUMN nome_colonna;



select * from Utenti
INSERT INTO Product(Id, Name, Description, Active, Price, DateRegister, Image, StockQuantity, CreatedAt, Deleted)
VALUES(NEWID(), 'Camiseta Yoda M', 'Camiseta StarWars 100% algodão', 1, 120, GETDATE(), '', 10, GETDATE(), 0);

INSERT INTO Product(Id, Name, Description, Active, Price, DateRegister, Image, StockQuantity, CreatedAt, Deleted)
VALUES(NEWID(), 'Funko Harry Potter', 'Funko da série de filmes Harry Potter', 1, 160, GETDATE(), '', 100, GETDATE(), 0);

INSERT INTO Product(Id, Name, Description, Active, Price, DateRegister, Image, StockQuantity, CreatedAt, Deleted)
VALUES(NEWID(), 'Caneca The Big Bang', 'Caneca da série The big bang theory', 1, 60, GETDATE(), '', 15, GETDATE(), 0);

SELECT * FROM Product;

INSERT INTO Address(Id, Street, Number, Complement, Neighborhood, PostalCode, City, State, CreatedAt, Deleted)
VALUES(NEWID(), 'Rua Monte Branco', '20', 'Qd. 3 Lt. 6', 'Bandeirante', '75690-000', 'Caldas Novas', 'GO', GETDATE(), 0);

INSERT INTO Address(Id, Street, Number, Complement, Neighborhood, PostalCode, City, State, CreatedAt, Deleted)
VALUES(NEWID(), 'Av. B', 'SN', 'Qd. 2 Lt. 5', 'Nova Vila', '75690-000', 'Caldas Novas', 'GO', GETDATE(), 0);

INSERT INTO Address(Id, Street, Number, Complement, Neighborhood, PostalCode, City, State, CreatedAt, Deleted)
VALUES(NEWID(), 'Orcalino Santos', 'SN', 'Qd. 32 Lt. 61', 'Santa Efigênia', '75690-000', 'Caldas Novas', 'GO', GETDATE(), 0);

SELECT * FROM Address;

INSERT INTO Client(Id, Name, Email, Cpf, Active, AddressId, CreatedAt, Deleted)
VALUES(NEWID(), 'José Martins', 'jose.martins@gmail.com', '12345678901', 1, 'C541989A-C517-4BFD-AFCD-C1AB6E5E70C4', GETDATE(), 0);

INSERT INTO Client(Id, Name, Email, Cpf, Active, AddressId, CreatedAt, Deleted)
VALUES(NEWID(), 'Mario José', 'mario.jose@gmail.com', '10987654321', 1, 'D00FA143-4E89-4FB8-9FD5-BB4AD1FC7808', GETDATE(), 0);

INSERT INTO Client(Id, Name, Email, Cpf, Active, AddressId, CreatedAt, Deleted)
VALUES(NEWID(), 'Maria Luiza', 'm.luiza@gmail.com', '00000000000', 1, '9DB584F3-DEDC-4BB6-B990-6683B5BB095C', GETDATE(), 0);

SELECT * FROM Client;
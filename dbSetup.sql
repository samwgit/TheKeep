-- Active: 1666715476024@@SG-sambcw-userdb-6830-mysql-master.servers.mongodirector.com@3306@garfieldgaming

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User Cover Image'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT DEFAULT 0,
        kept INT DEFAULT 0,
        Foreign Key (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        Foreign Key (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        Foreign Key (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        Foreign Key (creatorId) REFERENCES accounts(id)
    ) default charset utf8 comment '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500),
        img VARCHAR(255) COMMENT 'User Cover Image',
        isPrivate BOOLEAN NOT NULL DEFAULT 0
    ) default charset utf8 comment '';
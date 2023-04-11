CREATE DATABASE number_guess;

USE number_guess;

CREATE TABLE dbo.games (
    game_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    count_guesses INT NOT NULL,
    user_id INT
);


CREATE TABLE dbo.users (
    user_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(25) NOT NULL
);


ALTER TABLE dbo.users
    ADD CONSTRAINT users_username_key UNIQUE (username);

ALTER TABLE dbo.games
    ADD CONSTRAINT games_user_id_fkey FOREIGN KEY (user_id) REFERENCES dbo.users(user_id);

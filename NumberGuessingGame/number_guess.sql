CREATE DATABASE number_guess;

USE number_guess;

CREATE TABLE dbo.games (
    game_id integer PRIMARY KEY NOT NULL,
    count_guesses integer NOT NULL,
    user_id integer
);

CREATE TABLE dbo.users (
    user_id integer PRIMARY KEY NOT NULL,
    username character varying(25) NOT NULL
);

ALTER TABLE dbo.users
    ADD CONSTRAINT users_username_key UNIQUE (username);

ALTER TABLE dbo.games
    ADD CONSTRAINT games_user_id_fkey FOREIGN KEY (user_id) REFERENCES dbo.users(user_id);


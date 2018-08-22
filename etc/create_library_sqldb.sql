--Create Library Database
USE library;

--DROP TABLE poll;
--DROP TABLE reading_list;
--DROP TABLE book;
--DROP TABLE forum;
--DROP TABLE [user];

CREATE TABLE [user] (
  username varchar(32) NOT NULL,
  password nvarchar(32) NOT NULL,
  is_admin bit NOT NULL,
  newsletter bit,
  email nvarchar(max),
  CONSTRAINT pk_user_username PRIMARY KEY (username)
);

CREATE TABLE forum (
  post_id integer identity NOT NULL,
  username varchar(32) NOT NULL,
  message varchar(max) NOT NULL,
  CONSTRAINT fk_forum_username FOREIGN KEY (username) REFERENCES [user](username),
  CONSTRAINT pk_forum_post_id PRIMARY KEY (post_id)
  );

CREATE TABLE book (
  book_id integer identity NOT NULL,
  authors varchar(max) NOT NULL,
  title varchar(max) NOT NULL,
  genre varchar(max) NOT NULL,
  shelf_number integer NOT NULL,
  add_date datetime NOT NULL,
  CONSTRAINT pk_book_book_id PRIMARY KEY (book_id)
  );

CREATE TABLE reading_list (
  username varchar(32) NOT NULL,
  book_id integer NOT NULL,
  read_status bit,
  CONSTRAINT fk_reading_list_username FOREIGN KEY (username) REFERENCES [user](username),
  CONSTRAINT fk_reading_list_book_id FOREIGN KEY (book_id) REFERENCES book(book_id),
  CONSTRAINT pk_reading_list_username_book_id PRIMARY KEY (username, book_id)
);

CREATE TABLE poll (
  poll_id integer identity NOT NULL,
  username varchar(32) NOT NULL,
  book_id_for_favorite_title integer,
  book_id_for_favorite_authors integer,
  week_of date NOT NULL,
  CONSTRAINT fk_poll_book_id_for_favorite_title FOREIGN KEY (book_id_for_favorite_title) REFERENCES book(book_id),
  CONSTRAINT fk_poll_book_id_for_favorite_authors FOREIGN KEY (book_id_for_favorite_authors) REFERENCES book(book_id),
  CONSTRAINT fk_poll_username FOREIGN KEY (username) REFERENCES [user](username),
  CONSTRAINT pk_poll_poll_id PRIMARY KEY (poll_id)
);
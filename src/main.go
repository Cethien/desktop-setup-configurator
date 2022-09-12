package main

import (
	"database/sql"
	"fmt"
	"log"

	_ "github.com/mattn/go-sqlite3"
)

func main() {
	// create / connect db
	db, _ := sql.Open("sqlite3", "./config.db")
	statement, _ := db.Prepare("CREATE TABLE IF NOT EXISTS HelloWorld(id INTEGER PRIMARY KEY AUTOINCREMENT, msg TEXT)")
	statement.Exec()

	// insert dummy data if empty
	var count int

	err := db.QueryRow("SELECT COUNT(*) FROM HelloWorld").Scan(&count)
	switch {
	case err != nil:
		log.Fatal(err)
	default:
		if count == 0 {
			statement, _ = db.Prepare("INSERT INTO HelloWorld (msg) VALUES (?)")
			statement.Exec("Hello World!")
			statement.Exec("Whatzupp World?")
			statement.Exec("Another Hello World!!!")
			statement.Exec("Me Happy!")
			log.Printf("Created Dummy Data")
		}
	}

	//print random hello world
	rows, _ := db.Query("SELECT msg FROM HelloWorld ORDER BY RANDOM() LIMIT 1")
	var msg string

	for rows.Next() {
		rows.Scan(&msg)
		fmt.Println(msg)
	}
}

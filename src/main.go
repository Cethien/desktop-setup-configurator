package main

import (
	"github.com/Cethien/dsc/cmd"
	"github.com/Cethien/dsc/data"
)

func main() {
	db, err := data.OpenDatabase()
	if err != nil {
		panic("Can't open the Database")
	}
	data.Migrate(db)
	cmd.Execute()
}

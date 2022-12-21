package data

import (
	"gorm.io/driver/sqlite"
	"gorm.io/gorm"
)

type Tag struct {
	gorm.Model
	Name        string
	Description string
	IsDefault   bool
}

type Command struct {
	gorm.Model
	CommandText string
}

func OpenDatabase() (*gorm.DB, error) {
	return gorm.Open(sqlite.Open("./config.db"))
}

func Migrate(db *gorm.DB) {
	db.AutoMigrate(&Command{})
}

func Setup(db *gorm.DB) {
	Migrate(db)
}

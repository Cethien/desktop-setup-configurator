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

	// //create stages
	// db.Create(&Stage{
	// 	Tag: Tag{
	// 		Name:        "install",
	// 		Description: "Installing Stage",
	// 		IsDefault:   true,
	// 	}})
	// db.Create(&Stage{
	// 	Tag: Tag{
	// 		Name:        "post",
	// 		Description: "Post Installation Stage. Configuring and stuff",
	// 	}})

	// //create profiles
	// db.Create(&Profile{
	// 	Tag: Tag{
	// 		Name:      "any",
	// 		IsDefault: true,
	// 	}})
	// db.Create(&Profile{
	// 	Tag: Tag{
	// 		Name: "work",
	// 	}})
	// db.Create(&Profile{
	// 	Tag: Tag{
	// 		Name: "private",
	// 	}})

	// //create environments
	// db.Create(&Environment{
	// 	Tag: Tag{
	// 		Name:        "any",
	// 		Description: "Can run anywhere",
	// 		IsDefault:   true,
	// 	}})
	// db.Create(&Environment{
	// 	Tag: Tag{
	// 		Name:        "win",
	// 		Description: "Windows Specific",
	// 	}})
	// db.Create(&Environment{
	// 	Tag: Tag{
	// 		Name:        "lx",
	// 		Description: "Linux Specific",
	// 	}})

}

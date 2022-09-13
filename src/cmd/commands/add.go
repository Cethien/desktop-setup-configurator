/*
Copyright © 2022 NAME HERE <EMAIL ADDRESS>
*/
package commands

import (
	"encoding/json"
	"fmt"
	"log"

	"github.com/Cethien/dsc/data"
	"github.com/spf13/cobra"
)

var (
	commandText string
)

// addCmd represents the add command
var addCmd = &cobra.Command{
	Use:   "add",
	Short: "add a command",
	Long:  `add a command`,
	Run: func(cmd *cobra.Command, args []string) {
		db, _ := data.OpenDatabase()

		//get command text
		commandText = args[0]

		//create command
		command := &data.Command{
			CommandText: commandText,
		}
		db.Create(&command)

		fmt.Println("command added successfully!")

		b, _ := json.MarshalIndent(command, "", "\t")
		log.Println(string(b))
	},
}

func init() {}

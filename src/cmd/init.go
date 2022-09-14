/*
Copyright © 2022 NAME HERE <EMAIL ADDRESS>
*/
package cmd

import (
	"log"
	"os"

	"github.com/Cethien/dsc/data"
	"github.com/spf13/cobra"
)

// initCmd represents the init command
var initCmd = &cobra.Command{
	Use:   "init",
	Short: "Initializes a new local Database !!! DELETES EXISTING !!!",
	Long: `Initializes a new local Database
	
	WARNING: existing Database will be DELETED!`,
	Run: func(cmd *cobra.Command, args []string) {
		os.Remove("./config.db")
		log.Println("removed db file")
		db, _ := data.OpenDatabase()
		data.Setup(db)
		log.Println("created default data")
	},
}

func init() {
	rootCmd.AddCommand(initCmd)

	// Here you will define your flags and configuration settings.

	// Cobra supports Persistent Flags which will work for this command
	// and all subcommands, e.g.:
	// initCmd.PersistentFlags().String("foo", "", "A help for foo")

	// Cobra supports local flags which will only run when this command
	// is called directly, e.g.:
	// initCmd.Flags().BoolP("toggle", "t", false, "Help message for toggle")
}

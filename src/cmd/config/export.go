package config

import (
	"encoding/json"
	"os"

	"github.com/Cethien/dsc/data"
	"github.com/spf13/cobra"
)

type Config struct {
	InstallCommands []string `json:"install_commands"`
}

// exportCmd represents the export command
var exportCmd = &cobra.Command{
	Use:   "export",
	Short: "exports a new config.json from data",
	Long:  `exports a new config.json from data`,
	Run: func(cmd *cobra.Command, args []string) {
		db, _ := data.OpenDatabase()

		var commands []data.Command
		rows, _ := db.Find(&commands).Rows()

		var output Config

		for rows.Next() {
			var c data.Command
			db.ScanRows(rows, &c)
			output.InstallCommands = append(output.InstallCommands, c.CommandText)
		}

		b, _ := json.MarshalIndent(output, "", "\t")

		os.WriteFile("./config.json", b, 0644)
	},
}

func init() {

}

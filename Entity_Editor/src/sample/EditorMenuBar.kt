package sample

import javafx.scene.control.Menu
import javafx.scene.control.MenuBar
import javafx.scene.control.MenuItem
import javafx.scene.layout.VBox

class EditorMenuBar: VBox {
    constructor(){
        val menu = MenuBar()

        val file = Menu("File")
        val edit = Menu("Edit")
        menu.menus.addAll(file, edit)

        this.children.add(menu)
    }
}
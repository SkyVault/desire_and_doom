package sample;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Rectangle2D;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.HBox;
import javafx.stage.Screen;
import javafx.stage.Stage;

public class Main extends Application {

    @Override
    public void start(Stage window) throws Exception{
        Rectangle2D screen = Screen.getPrimary().getVisualBounds();
        window.setTitle("Entity Editor");
        HBox hbox = new HBox();
        hbox.getChildren().add(new EditorMenuBar());


        Scene scene = new Scene(hbox, 320, 800);
        window.setScene(scene);
        window.setX((screen.getWidth() / 2) - 320);
        window.setY((screen.getHeight() / 2) - (800 / 2) + 26);
        window.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}

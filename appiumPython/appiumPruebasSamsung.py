from appium import webdriver
from appium.options.android import UiAutomator2Options
import time

def main():
    print("Conectando a Appium...")

    options = UiAutomator2Options().load_capabilities({
        "platformName": "Android",
        "automationName": "UiAutomator2",
        "deviceName": "Android",
        "udid": "R58N14N77FZ",  # PON EL UDID DE TU TELÉFONO
        "appPackage": "com.android.settings",  # Abrirá Configuración
        "appActivity": ".Settings",
        "noReset": True
    })

    driver = webdriver.Remote(
        "http://127.0.0.1:4725",  # <--- PUERTO ACTUALIZADO
        options=options
    )

    print("App abierta correctamente!")
    time.sleep(5)

    driver.quit()

if __name__ == "__main__":
    main()

from appium import webdriver
from appium.options.android import UiAutomator2Options
from selenium.webdriver.common.by import By
import time

def main():
    print("Conectando a Appium...")

    options = UiAutomator2Options().load_capabilities({
        "platformName": "Android",
        "automationName": "UiAutomator2",
        "deviceName": "Xiaomi",
        "udid": "scso7txwh6lrjbqs",  
        "appPackage": "com.miui.calculator",
        "appActivity": "com.miui.calculator.cal.CalculatorActivity",
        "noReset": True,
        "ignoreHiddenApiPolicyError": True,
        "autoGrantPermissions": True
    })

    driver = webdriver.Remote(
        "http://127.0.0.1:4725",  # Puerto Appium
        options=options
    )

    time.sleep(2)

    # ➤ Presionar 2
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    time.sleep(1)

    # ➤ Presionar +
    driver.find_element(By.ID, "com.miui.calculator:id/btn_plus_s").click()
    time.sleep(1)

    # ➤ Presionar 5
    driver.find_element(By.ID, "com.miui.calculator:id/btn_5_s").click()
    time.sleep(1)

    # ➤ Presionar =
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    time.sleep(1)

    # ➤ Obtener resultado (el ID puede variar, este es el estándar)
    try:
        resultado = driver.find_element(By.ID, "com.miui.calculator:id/result").text
        print("Resultado:", resultado)
    except:
        print("⚠️ No se encontró el ID del resultado. Envíame captura de pantalla o el ID real.")

    time.sleep(3)
    driver.quit()


if __name__ == "__main__":
    main()

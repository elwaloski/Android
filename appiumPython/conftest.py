import pytest
import allure
from appium import webdriver
from appium.options.android import UiAutomator2Options
import time
import datetime


@pytest.fixture
def driver():
    options = UiAutomator2Options().load_capabilities({
        "platformName": "Android",
        "automationName": "UiAutomator2",
        #"deviceName": "Xiaomi",
        "udid": "scso7txwh6lrjbqs",
        "deviceName": "Xiaomi-Wifi",
        #"udid": "adb-scso7txwh6lrjbqs-xPSg8r._adb-tls-connect._tcp", 
        #"udid":"emulator-5554"
        "appPackage": "com.miui.calculator",
        "appActivity": "com.miui.calculator.cal.CalculatorActivity",
        "noReset": True,
        "ignoreHiddenApiPolicyError": True,
        "autoGrantPermissions": True
    })

    driver = webdriver.Remote("http://127.0.0.1:4723", options=options)
    time.sleep(2)
    yield driver
    driver.quit()

# Se ejecuta AUTOMÁTICAMENTE cuando un test falla
@pytest.hookimpl(hookwrapper=True)
def pytest_runtest_makereport(item, call):
    outcome = yield
    result = outcome.get_result()

    if result.when == "call" and result.failed:
        driver = item.funcargs.get("driver")
        if driver:
            timestamp = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
            nombre = f"FAILED_{item.name}_{timestamp}.png"

            driver.save_screenshot(nombre)

            # 🔥 ADJUNTAR A ALLURE
            allure.attach.file(
                nombre,
                name=f"Screenshot - {item.name}",
                attachment_type=allure.attachment_type.PNG
            )

            print(f"[ERROR] Screenshot guardado y adjuntado a Allure: {nombre}")
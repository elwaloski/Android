import allure
from selenium.webdriver.common.by import By


def limpiar(driver):
    driver.find_element(By.ID, "com.miui.calculator:id/btn_c_s").click()


def obtener_resultado(driver):
    resultado = driver.find_element(By.ID, "com.miui.calculator:id/result").text
    return resultado.replace("=", "").replace(" ", "").replace(".", "").strip()


@allure.feature("Operaciones")
@allure.story("Suma")
def test_suma(driver):
    with allure.step("Limpiar calculadora"):
        limpiar(driver)

    with allure.step("Realizar operación 2 + 5"):
        driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
        driver.find_element(By.ID, "com.miui.calculator:id/btn_plus_s").click()
        driver.find_element(By.ID, "com.miui.calculator:id/btn_5_s").click()
        driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()

    assert obtener_resultado(driver) == "7"


@allure.feature("Operaciones")
@allure.story("Multiplicación")
def test_multiplicacion(driver):
    limpiar(driver)

    driver.find_element(By.ID, "com.miui.calculator:id/btn_3_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_mul_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_4_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()

    assert obtener_resultado(driver) == "12"


@allure.feature("Operaciones")
@allure.story("División")
def test_division(driver):
    limpiar(driver)

    driver.find_element(By.ID, "com.miui.calculator:id/btn_8_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_div_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()

    assert obtener_resultado(driver) == "4"


@allure.feature("Operaciones")
@allure.story("DivisiónMala")
def test_divisionMala(driver):
    limpiar(driver)

    driver.find_element(By.ID, "com.miui.calculator:id/btn_8_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_div_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()

    # Este va a fallar y sacará screenshot automáticamente
    assert obtener_resultado(driver) == "444"


@allure.feature("Operaciones")
@allure.story("Resta")
def test_resta(driver):
    limpiar(driver)

    driver.find_element(By.ID, "com.miui.calculator:id/btn_5_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_minus_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()

    assert obtener_resultado(driver) == "3"

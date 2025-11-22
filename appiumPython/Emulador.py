from selenium.webdriver.common.by import By
import time

def limpiar(driver):
    driver.find_element(By.ID, "com.miui.calculator:id/btn_c_s").click()
    time.sleep(1)

def obtener_resultado(driver):
    resultado = driver.find_element(By.ID, "com.miui.calculator:id/result").text
    return resultado.replace("=", "").replace(" ", "").replace(".", "").strip()

def test_suma(driver):
    limpiar(driver)
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_plus_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_5_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    assert obtener_resultado(driver) == "7"

def test_multiplicacion(driver):
    limpiar(driver)
    driver.find_element(By.ID, "com.miui.calculator:id/btn_3_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_mul_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_4_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    assert obtener_resultado(driver) == "12"

def test_division(driver):
    limpiar(driver)
    driver.find_element(By.ID, "com.miui.calculator:id/btn_8_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_div_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    assert obtener_resultado(driver) == "4"

def test_RestaMala(driver):
    limpiar(driver)
    driver.find_element(By.ID, "com.miui.calculator:id/btn_8_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_minus_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    assert obtener_resultado(driver) == "4"  # Este fallará y sacará screenshot

def test_Resta(driver):
    limpiar(driver)
    driver.find_element(By.ID, "com.miui.calculator:id/btn_8_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_minus_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_2_s").click()
    driver.find_element(By.ID, "com.miui.calculator:id/btn_equal_s").click()
    assert obtener_resultado(driver) == "6"

#com.miui.calculator:id/btn_percent_s
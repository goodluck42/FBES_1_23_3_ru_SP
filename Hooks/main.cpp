#include <Windows.h>
#include <iostream>
#include <cstdlib>
#include <string>


LRESULT KeyboardHookProc(int code, WPARAM wParam, LPARAM lParam)
{
    switch (wParam)
    {
    case WM_KEYDOWN:
        {
            POINT point;

            GetCursorPos(&point);

            std::wstring str;

            str += std::to_wstring(point.x);
            str += L" ";
            str += std::to_wstring(point.y);

            MessageBoxEx(NULL, str.c_str(), TEXT("Hooks"), MB_OK, NULL);

            break;
        }
    default:
        {
            return CallNextHookEx(NULL, code, wParam, lParam);
        }
    }

    return 0;
}

LRESULT MouseHookProc(int code, WPARAM wParam, LPARAM lParam)
{
    if (wParam != WM_MOUSEMOVE)
    {
        return CallNextHookEx(NULL, code, wParam, lParam);
    }

    POINT point;

    GetCursorPos(&point);

    if (point.x >= 1023)
    {
        SetCursorPos(1022, point.y);

        return -1;
    }

    if (point.y >= 575)
    {
        SetCursorPos(point.x, 574);

        return -1;
    }

    return 0;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR lpCmdLine, int nCmdShow)
{
    auto mouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProc, hInstance, NULL);
    auto keyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProc, hInstance, NULL);

    MSG msg;

    while (GetMessage(&msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
}

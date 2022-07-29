
// MFCAppStdSDI1.h : main header file for the MFCAppStdSDI1 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CMFCAppStdSDIApp:
// See MFCAppStdSDI1.cpp for the implementation of this class
//

class CMFCAppStdSDIApp : public CWinApp
{
public:
	CMFCAppStdSDIApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CMFCAppStdSDIApp theApp;

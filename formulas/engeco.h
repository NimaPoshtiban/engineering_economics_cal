#undef ENGECO_H
#define ENGECO_H
#pragma once

#ifdef ENGECO_H_EXPORTS
#define ENGECO_H_API __declspec(dllexport)
#else
#define ENGECO_H_API __declspec(dllimport)
#endif
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

extern "C" ENGECO_H_API double calculate_summation(int start, int end, double interest,
                                  double annual_uniform);

extern "C" ENGECO_H_API double
calculate_summation_via_geometric_progression(double a1, double r,
                                              double annual_uniform, int n);

extern "C" ENGECO_H_API double calculate_interest(double f, double p, int n);

extern "C" ENGECO_H_API double calculate_interest_amount(double p, double i);
extern "C" ENGECO_H_API double calculate_future(double p, double i, int n);

extern "C" ENGECO_H_API double calcualate_future_via_annual_uniform(double annual_uniform,
                                                   double interest, int n);

extern "C" ENGECO_H_API double calculate_ror(double benefit, double present);

extern "C" ENGECO_H_API int calculate_n(double i, double f, double p);

extern "C" ENGECO_H_API double calculate_present(double f, double i, double n);

extern "C" ENGECO_H_API double calculate_present_via_annual_uniform(double annual_uniform,
                                                   double interest, int n);

extern "C" ENGECO_H_API double calculate_present_via_gradient(double gradient, double interest,
                                             int n);
extern "C" ENGECO_H_API double calculate_summation_with_gradient(double gradient,
                                                double annual_uniform,
                                                double interest, int n);
extern "C" ENGECO_H_API double calculate_present_via_geometric_gradient(double a1, double i,
                                                       double g, int n);

extern "C" ENGECO_H_API double calculate_annual_uniform_via_present(double present,
                                                   double interest, int n);

extern "C" ENGECO_H_API double calculate_annual_uniform_via_future(double future,
                                                  double interest, int n);

extern "C" ENGECO_H_API double calculate_annual_uniform_via_gradient(double gradient,
                                                    double interest, int n);

extern "C" ENGECO_H_API  double calculate_effective_interest_rate(double r, double m);
extern "C" ENGECO_H_API double calculate_effective_interest_rate_c(double r);

// Continuous Compounding
extern "C" ENGECO_H_API double calculate_future_c(double p, double r, int n);

extern "C" ENGECO_H_API double calculate_present_c(double f, double r, double n);

extern "C" ENGECO_H_API double calcualate_future_via_annual_uniform_c(double a, double r, int n);

extern "C" ENGECO_H_API double calculate_present_via_annual_uniform_c(double a, double r, int n);
extern "C" ENGECO_H_API double calculate_annual_uniform_via_present_c(double p, double r, int n);
extern "C" ENGECO_H_API double calculate_annual_uniform_via_future_c(double f, double r, int n);
extern "C" ENGECO_H_API double calculate_annual_uniform_via_gradient_c(double g, double r,
                                                      int n);

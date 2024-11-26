#include "engeco.h"


extern double calculate_summation(int start, int end, double interest,
                                  double annual_uniform) {
  register double total = 0;
  for (int i = start; i <= end; i++) {
    total += (annual_uniform / (pow(1 + interest, i)));
  }
  return total;
}

extern double
calculate_summation_via_geometric_progression(double a1, double r,
                                              double annual_uniform, int n) {
  return annual_uniform * (a1 * ((1 - pow(r, n)) / (1 - r)));
}
extern double calculate_interest(double f, double p, int n) {
  return (pow(f / p, 1.0 / n) - 1);
}
extern double calculate_interest_amount(double p, double i) { return p * i; }
extern double calculate_future(double p, double i, int n) {
  return p * pow(1 + i, n);
}
extern double calcualate_future_via_annual_uniform(double annual_uniform,
                                                   double interest, int n) {
  return annual_uniform * ((pow(1 + interest, n) - 1) / interest);
}
extern double calculate_ror(double benefit, double present) {
  return benefit / present * 100;
}
extern int calculate_n(double i, double f, double p) {
  return ceil(log(f / p) / log(i + 1));
}
extern double calculate_present(double f, double i, double n) {
  return f / pow(1 + i, n);
}
extern double calculate_present_via_annual_uniform(double annual_uniform,
                                                   double interest, int n) {
  return annual_uniform *
         ((pow(1 + interest, n) - 1) / (interest * pow(1 + interest, n)));
}

extern double calculate_present_via_gradient(double gradient, double interest,
                                             int n) {
  return (gradient / interest) *
         (((pow(1 + interest, n) - 1) / (interest * pow(1 + interest, n))) -
          (n / pow(1 + interest, n)));
}

extern double calculate_summation_with_gradient(double gradient,
                                                double annual_uniform,
                                                double interest, int n) {
  return calculate_present_via_gradient(gradient, interest, n) +
         calculate_present_via_annual_uniform(annual_uniform, interest, n);
}

extern double calculate_present_via_geometric_gradient(double a1, double i,
                                                       double g, int n) {
  if (i != g) {
    return a1 * ((1 - (pow(1 + g, n) * pow(1 + i, -n))) / (i - g));
  }
  if (i == g) {
    return a1 * (n * pow(1 + i, -1));
  }
  return EXIT_FAILURE;
}
extern double calculate_annual_uniform_via_present(double present,
                                                   double interest, int n) {
  return present *
         ((interest * pow(1 + interest, n)) / (pow(1 + interest, n) - 1));
}
extern double calculate_annual_uniform_via_future(double future,
                                                  double interest, int n) {
  return future * (interest / (pow(1 + interest, n) - 1));
}

extern double calculate_annual_uniform_via_gradient(double gradient,
                                                    double interest, int n) {
  return gradient * ((1 / interest) - (n / (pow(interest + 1, n) - 1)));
}

extern double calculate_effective_interest_rate(double r, double m) {
  return pow(1 + r / m, m) - 1;
}

extern double calculate_effective_interest_rate_c(double r) {
  return exp(r) - 1;
}
// Continuous Compounding
extern double calculate_future_c(double p, double r, int n) {
  return p * exp(r * n);
}
extern double calculate_present_c(double f, double r, double n) {
  return f / exp(r * n);
}

extern double calcualate_future_via_annual_uniform_c(double a, double r,
                                                     int n) {
  return a * ((exp(r * n) - 1) / (exp(r) - 1));
}
extern double calculate_present_via_annual_uniform_c(double a, double r,
                                                     int n) {
  return a * ((exp(r * n)-1) / (exp(r*n)* (exp(r) - 1)));
}


extern double calculate_annual_uniform_via_present_c(double p, double r, int n) {
  return p * ((exp(r*n)*(exp(r) - 1) / (exp(r * n)-1)));
}
extern double calculate_annual_uniform_via_future_c(double f, double r, int n) {

  return f * ((exp(r) - 1) / (exp(r * n) - 1));
}
extern double calculate_annual_uniform_via_gradient_c(double g, double r,
                                                      int n) {
  return g * (((1) / (exp(r) - 1)) - ((n) / (exp(r * n) - 1)));
}

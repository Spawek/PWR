function [ w ] = make_learning_step( w, probes, training_results, how_far_forward_to_forecast)

%% constants
random_change_val = 0.007;
random_change_multipler = 0.05;

%% helpers
probe_size = size(probes, 1);

%% road speed prediction basing on curr weights
predictions = make_predictions(w, probes);
sqr_error = mean((predictions(1:end-how_far_forward_to_forecast) - training_results).^2);

%% 'gradient calculating' 
random_weight_index = ceil(rand() * probe_size);
w(random_weight_index) = w(random_weight_index) + random_change_val;

%% road speed prediction basing on curr weights
predictions = make_predictions(w, probes);  
new_sqr_error = mean((predictions(1:end-how_far_forward_to_forecast) - training_results).^2);
error_diff = new_sqr_error - sqr_error;

%% resetting weights matrix
w(random_weight_index) = w(random_weight_index) - random_change_val;

%% weights matrix change
calculated_weight_change = random_change_val * error_diff * random_change_multipler;
w(random_weight_index) = w(random_weight_index) - calculated_weight_change;

end


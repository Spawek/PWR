function [ prediction ] = forecast_road_speed( training_data, work_data )

%% constants declaration
backward_data_usage_depth = 4; % app will use actual data this no backward
road_you_need_speed_on = 3;
random_change_val = 0.007;
random_change_multipler = 0.05;

% no of times that every nauron will be recalculated
% (neurons are recalculated in random permutation)
learning_iterations = 500;

%% fix data taken from speed.xls
training_data = fix_input_data(training_data);

%% calculating helpers
no_of_roads = size(training_data, 1);
training_data_points_no = size(training_data, 2) - (backward_data_usage_depth + 1);
probe_size = no_of_roads * (backward_data_usage_depth + 1);

%% creating 'random' weights vector

% weights vector is rand in range [0, 2/no_of_roads]
% TODO: it should be multiplied by (avg on  road / overall avg)
w = 2 / probe_size * rand(probe_size, 1);

%% creating probes from training data
% each column is 1 input point, each for loop iteration adds 1 column
probes = [];
for i = backward_data_usage_depth + 1:size(training_data, 2)
    probe = [];
    for j = 0 : backward_data_usage_depth
       probe = [probe; training_data(:, i - j)];
    end    
    probes = [probes, probe];
end

%% getting training_results arr from training data
training_results = training_data(road_you_need_speed_on, backward_data_usage_depth+2:end)';

%% network learning!
predictions = zeros(training_data_points_no,1);
for i = 1:training_data_points_no 
    predictions(i) = sum(w .* probes(:,i));
end
avg_error_before = mean((predictions - training_results).^2);

for iterations = 1:learning_iterations*probe_size; %TODO: its complately random order - remake it!
    % road speed prediction basing on curr weights
    predictions = zeros(training_data_points_no,1);
    for i = 1:training_data_points_no 
        predictions(i) = sum(w .* probes(:,i));
    end
    avg_error = mean(predictions - training_results);
    sqr_error = mean((predictions - training_results).^2);

    random_weight_index = ceil(rand() * probe_size);

    % 'gradient calculating' - let's say
    w(random_weight_index) = w(random_weight_index) + random_change_val;

    % road speed prediction basing on curr weights
    predictions = zeros(training_data_points_no,1);
    for i = 1:training_data_points_no 
        predictions(i) = sum(w .* probes(:,i));
    end
    new_avg_error = mean(predictions - training_results);
    new_sqr_error = mean((predictions - training_results).^2);
    
    %error_diff = new_avg_error - avg_error;
    error_diff = new_sqr_error - sqr_error;

    % resetting weights matrix
    w(random_weight_index) = w(random_weight_index) - random_change_val;

    % weights matrix change
    calculated_weight_change = random_change_val * error_diff * random_change_multipler;
    w(random_weight_index) = w(random_weight_index) - calculated_weight_change;% * sign(avg_error);
    
    iterations
end
predictions = zeros(training_data_points_no,1);
for i = 1:training_data_points_no 
    predictions(i) = sum(w .* probes(:,i));
end
predictions - training_results
avg_error_before
avg_error_after = mean((predictions - training_results).^2)

clf
plot([0;0;0;0;0;0;predictions]','r')
hold on
plot(training_data(road_you_need_speed_on,:),'b')


end
